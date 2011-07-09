// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Olive.svc.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the Olive type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.ServiceModel;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;

    public class WebService : IWebService
    {
        [Microsoft.Practices.Unity.Dependency]
        public virtual IUnityContainer Container { get; set; }

        public int CreateCurrentAccount(Guid sessionId, string currencyId, string displayName)
        {
            if (displayName == string.Empty)
            {
                displayName = null;
            }

            var userId = this.VerifySession(sessionId);

            using (var context = this.GetContext())
            {
                return context.CreateCurrentAccount(userId, currencyId, displayName);
            }
        }

        public Guid CreateSession(string email, string password)
        {
            using (var context = this.GetContext())
            {
                var salt =
                    context.Users.Where(u => u.Email.ToLower() == email.ToLower()).Select(u => u.PasswordSalt).FirstOrDefault();

                if (salt == null)
                {
                    throw this.FaultFactory.CreateUnrecognizedCredentialsException(email);
                }

                var crypto = this.Container.Resolve<ICrypto>();

                var hash = crypto.GenerateHash(password, salt);

                try
                {
                    return context.CreateSession(email, hash);
                }
                catch (AuthenticationException)
                {
                    throw this.FaultFactory.CreateUnrecognizedCredentialsException(email);
                }
            }
        }

        public long CreateTransfer(
            Guid sessionId, int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            var userId = default(int);

            userId = this.VerifySession(sessionId);

            using (var context = this.GetContext())
            {
                var hasAccess = this.UserCanWithdrawFromAccount(userId, sourceAccountId);

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedAccountWithdrawFaultException(userId, sourceAccountId);
                }

                try
                {
                    return context.CreateTransfer(sourceAccountId, destAccountId, description, amount);
                }
                catch (AuthorizationException)
                {
                    throw this.FaultFactory.CreateUnauthorizedAccountWithdrawFaultException(userId, sourceAccountId);
                }
            }
        }

        public virtual bool EmailIsRegistered(string email)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Any(u => u.Email.ToLower() == email.ToLower());
            }
        }

        [Dependency]
        public IFaultFactory FaultFactory { get; set; }

        public virtual void CreateUser(string email, string password)
        {
            using (var context = this.GetContext())
            {
                if (this.EmailIsRegistered(email))
                {
                    throw this.FaultFactory.CreateEmailAlreadyRegisteredFaultException(email);
                }

                var crypto = this.Container.Resolve<ICrypto>();

                var salt = crypto.CreateSalt(64);
                var hash = crypto.GenerateHash(password, salt);

                var user = new User { PasswordHash = hash, PasswordSalt = salt, Email = email };
                context.Users.Add(user);
                context.SaveChanges();

                return; ////user.UserId;
            }
        }

        public void EditCurrentAccount(Guid sessionId, int accountId, string displayName)
        {
            var userId = default(int);

            try
            {
                userId = this.VerifySession(sessionId);
            }
            catch (AuthenticationException)
            {
                throw this.FaultFactory.CreateSessionDoesNotExistFaultException(sessionId);
            }

            using (var context = this.GetContext())
            {
                var hasAccess = this.UserCanEditAccount(userId, accountId);

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedAccountEditFaultException(userId, accountId);
                }

                try
                {
                    context.EditCurrentAccount(accountId, displayName);
                }
                catch (AuthorizationException)
                {
                    throw this.FaultFactory.CreateUnauthorizedAccountEditFaultException(userId, accountId);
                }
            }
        }

        public GetAccountAccount GetAccount(Guid sessionId, int accountId)
        {
                var userId = this.VerifySession(sessionId);

                using (var context = this.GetContext())
                {
                    var hasAccess = this.UserCanViewAccount(userId, accountId);

                    if (!hasAccess)
                    {
                        throw this.FaultFactory.CreateUnauthorizedAccountAccessFaultException(userId, accountId);
                    }

                    var account = context.Accounts.Find(accountId);

                    return new GetAccountAccount
                        {
                            AccountId = account.AccountId,
                            DisplayName = account.DisplayName,
                            CurrencyId = account.CurrencyId,
                            AccountType = account.AccountType
                        };
                }
        }

        public List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId)
        {
            throw new NotImplementedException();
        }

        public AccountOverview GetAccounts(Guid sessionId)
        {
            var userId = this.VerifySession(sessionId);

            using (var context = this.GetContext())
            {
                var query = from account in context.Users.Find(userId).AccountAccess.Select(x => x.Account)
                            let accountWithBalance = context.AccountsWithBalance.Find(account.AccountId)
                            select
                                new AccountOverviewAccount
                                    {
                                        AccountId = account.AccountId, 
                                        Balance = accountWithBalance.Balance, 
                                        CurrencyId = accountWithBalance.Currency.CurrencyId, 
                                        DisplayName = account.DisplayName
                                    };

                var result = new AccountOverview();
                result.AddRange(query);
                return result;
            }
        }

        public List<string> GetCurrencies()
        {
            using (var context = this.GetContext())
            {
                return context.Currencies.Select(x => x.CurrencyId).ToList();
            }
        }

        public virtual bool UserCanEditAccount(int userId, int accountId)
        {
            return this.UserCanWithdrawFromAccount(userId, accountId);
        }

        /// <summary>
        /// Returns whether the specified user has access to withdraw money from the specified account.
        /// </summary>
        /// <param name="userId">The user whose access is being checked.</param>
        /// <param name="accountId">The account to which access is required.</param>
        /// <returns>True if the account exists and the user can withdraw from it else False.</returns>
        public virtual bool UserCanWithdrawFromAccount(int userId, int accountId)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Find(userId).AccountAccess.Any(x => x.CanWithdraw && x.AccountId == accountId);
            }
        }

        private IOliveContext GetContext()
        {
            Contract.Requires(this.Container != null);
            Contract.Ensures(Contract.Result<IOliveContext>() != null);

            return this.Container.Resolve<IOliveContext>();
        }

        private int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentNullException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            using (var context = this.GetContext())
            {
                try
                {
                    return context.VerifySession(sessionId);
                }
                catch (SessionDoesNotExistException)
                {
                    throw this.FaultFactory.CreateSessionDoesNotExistFaultException(sessionId);
                }
            }
        }

        public virtual bool UserCanViewAccount(int userId, int accountId)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Find(userId).AccountAccess.Any(x => x.AccountId == accountId);
            }
        }
    }
}