// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Olive.svc.cs" company="Olive">
//   [Copyright]
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
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;

    public class WebService : IWebService
    {
        [Microsoft.Practices.Unity.Dependency]
        public IUnityContainer Container { get; set; }

        public WebService()
        {
        }

        private IOliveContext GetContext()
        {
            Contract.Requires(this.Container != null);
            Contract.Ensures(Contract.Result<IOliveContext>() != null);

            return this.Container.Resolve<IOliveContext>();
        }

        public Guid CreateSession(string email, string password)
        {
            using (var context = this.GetContext())
            {
                var salt = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).Select(u => u.PasswordSalt).FirstOrDefault();

                if (salt == null)
                {
                    throw new AuthenticationException();
                }

                var crypto = this.Container.Resolve<ICrypto>();

                var hash = crypto.GenerateHash(password, salt);

                return context.CreateSession(email, hash);
            }
        }

        public void CreateUser(string email, string password)
        {
            using (var context = this.GetContext())
            {
                if (context.Users.Any(u => u.Email.ToLower() == email.ToLower()))
                {
                    throw new EmailAlreadyRegisteredException();
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

        public List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId)
        {
            throw new NotImplementedException();
        }

        public GetAccountAccount GetAccount(Guid sessionId, int accountId)
        {
            throw new NotImplementedException();
        }

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

        public void EditAccount(Guid sessionId, int accountId, string displayName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetCurrencies()
        {
            using (var context = this.GetContext())
            {
                return context.Currencies.Select(x => x.CurrencyId).ToList();
            }
        }

        public virtual bool UserCanWithdrawFromAccount(int userId, int accountId)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Find(userId).AccountAccess.Any(
                    x => x.CanWithdraw && x.AccountId == accountId);
            }
        }

        public long CreateTransfer(Guid sessionId, int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            var userId = this.VerifySession(sessionId);

            using (var context = this.GetContext())
            {
                var hasAccess = this.UserCanWithdrawFromAccount(userId, sourceAccountId);

                if (!hasAccess)
                {
                    throw new FaultException("The user does not have permission to withdraw from the specified account.");
                }

                return context.CreateTransfer(sourceAccountId, destAccountId, description, amount);
            }
        }

        private int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentNullException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            using (var context = this.GetContext())
            {
                return context.VerifySession(sessionId);
            }
        }
    }
}
