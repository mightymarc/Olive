// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Olive.svc.cs" company="Olive">
//   Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    
//    1. Definitions
//    
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    
//    A "contribution" is the original software, or any additions or changes to the software.
//    
//    A "contributor" is any person that distributes its contribution under this license.
//    
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    
//    2. Grant of Rights
//    
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    
//    3. Conditions and Limitations
//    
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
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

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;

    /// <summary>
    /// The web service.
    /// </summary>
    public class WebService : IWebService
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Container.
        /// </summary>
        [Microsoft.Practices.Unity.Dependency]
        public virtual IUnityContainer Container { get; set; }

        /// <summary>
        /// Gets or sets FaultFactory.
        /// </summary>
        [Dependency]
        public IFaultFactory FaultFactory { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The create current account.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="currencyId">
        /// The currency id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// The create current account.
        /// </returns>
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

        /// <summary>
        /// The create session.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        /// <exception cref="FaultException">
        /// </exception>
        public Guid CreateSession(string email, string password)
        {
            using (var context = this.GetContext())
            {
                var salt =
                    context.Users.Where(u => u.Email.ToLower() == email.ToLower()).Select(u => u.PasswordSalt).
                        FirstOrDefault();

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

        /// <summary>
        /// The create transfer.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="sourceAccountId">
        /// The source account id.
        /// </param>
        /// <param name="destAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        /// <exception cref="FaultException">
        /// </exception>
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

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <exception cref="FaultException">
        /// </exception>
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

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <exception cref="FaultException">
        /// </exception>
        /// <exception cref="FaultException">
        /// </exception>
        /// <exception cref="FaultException">
        /// </exception>
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

        /// <summary>
        /// The email is registered.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The email is registered.
        /// </returns>
        public virtual bool EmailIsRegistered(string email)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Any(u => u.Email.ToLower() == email.ToLower());
            }
        }

        /// <summary>
        /// The get account.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
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

        /// <summary>
        /// The get account transfers.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get accounts.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// </returns>
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

        /// <summary>
        /// The get currencies.
        /// </summary>
        /// <returns>
        /// </returns>
        public List<string> GetCurrencies()
        {
            using (var context = this.GetContext())
            {
                return context.Currencies.Select(x => x.CurrencyId).ToList();
            }
        }

        /// <summary>
        /// The user can edit account.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// The user can edit account.
        /// </returns>
        public virtual bool UserCanEditAccount(int userId, int accountId)
        {
            return this.UserCanWithdrawFromAccount(userId, accountId);
        }

        /// <summary>
        /// The user can view account.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// The user can view account.
        /// </returns>
        public virtual bool UserCanViewAccount(int userId, int accountId)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Find(userId).AccountAccess.Any(x => x.AccountId == accountId);
            }
        }

        /// <summary>
        /// Returns whether the specified user has access to withdraw money from the specified account.
        /// </summary>
        /// <param name="userId">
        /// The user whose access is being checked.
        /// </param>
        /// <param name="accountId">
        /// The account to which access is required.
        /// </param>
        /// <returns>
        /// True if the account exists and the user can withdraw from it else False.
        /// </returns>
        public virtual bool UserCanWithdrawFromAccount(int userId, int accountId)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Find(userId).AccountAccess.Any(x => x.CanWithdraw && x.AccountId == accountId);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get context.
        /// </summary>
        /// <returns>
        /// </returns>
        private IOliveContext GetContext()
        {
            Contract.Requires(this.Container != null);
            Contract.Ensures(Contract.Result<IOliveContext>() != null);

            return this.Container.Resolve<IOliveContext>();
        }

        /// <summary>
        /// The verify session.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The verify session.
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
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

        #endregion
    }
}