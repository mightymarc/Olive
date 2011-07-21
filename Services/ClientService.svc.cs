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
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Transactions;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;
    using Olive.DataAccess.Domain;

    /// <summary>
    /// The web service.
    /// </summary>
    ////[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    // TODO: This should be in config, but it doesn't seem to pick it up.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
    public class ClientService : IClientService
    {
        /// <summary>
        ///   Gets or sets Container.
        /// </summary>
        [Dependency]
        public virtual IUnityContainer Container { get; set; }

        [Dependency]
        public virtual ICrypto Crypto { get; set; }

        /// <summary>
        ///   Gets or sets FaultFactory.
        /// </summary>
        [Dependency]
        public IFaultFactory FaultFactory { get; set; }


        /// <summary>
        /// The create current account.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="currencyId">The specialAccountName id.</param>
        /// <param name="displayName">The display name.</param>
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
        /// Gets the special account id.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public int GetSpecialAccountId(string name)
        {
            using (var context = this.GetContext())
            {
                return context.GetSpecialAccountId(name);
            }
        }

        /// <summary>
        /// The create session.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="FaultException">
        ///   </exception>
        ///   
        /// <exception cref="FaultException">
        ///   </exception>
        public Guid CreateSession(string email, string password)
        {
            Contract.Requires(this.FaultFactory != null, "FaultFactory dependency not resolved.");

            using (var context = this.GetContext())
            {
                var salt =
                    context.Users.Where(u => u.Email.ToLower() == email.ToLower()).Select(u => u.PasswordSalt).
                        FirstOrDefault();

                if (salt == null)
                {
                    throw this.FaultFactory.CreateUnrecognizedCredentialsException(email);
                }

                var hash = this.Crypto.GenerateHash(password, salt);

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

        private bool UserHasRole(int userId, string roleId)
        {
            using (var context = this.GetContext())
            {
                return context.Users.Find(userId).Roles.Any(r => r.RoleId == roleId);
            }
        }

        /// <summary>
        /// The create transfer.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="sourceAccountId">The source account id.</param>
        /// <param name="destAccountId">The dest account id.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        /// <exception cref="FaultException">
        ///   </exception>
        ///   
        /// <exception cref="FaultException">
        ///   </exception>
        public long CreateTransfer(
            Guid sessionId, int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            var userId = default(int);

            userId = this.VerifySession(sessionId);

            using (var context = this.GetContext())
            {
                var isAdmin = this.UserHasRole(userId, "Admin");
                var hasAccess = isAdmin || this.UserCanWithdrawFromAccount(userId, sourceAccountId);
                hasAccess &= isAdmin || this.UserCanDepositToAccount(userId, destAccountId);

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
        public virtual void CreateUser(string email, string password, int parentUserId)
        {
            using (var context = this.GetContext())
            {
                if (this.EmailIsRegistered(email))
                {
                    throw this.FaultFactory.CreateEmailAlreadyRegisteredFaultException(email);
                }

                var salt = this.Crypto.CreateSalt(64);
                var hash = this.Crypto.GenerateHash(password, salt);

                var user = new User { PasswordHash = hash, PasswordSalt = salt, Email = email, ParentUserId = parentUserId };
                context.Users.Add(user);
                context.SaveChanges();

                return; ////user.UserId;
            }
        }

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="displayName">The display name.</param>
        /// <exception cref="FaultException">
        ///   </exception>
        ///   
        /// <exception cref="FaultException">
        ///   </exception>
        ///   
        /// <exception cref="FaultException">
        ///   </exception>
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
                var hasAccess = this.UserHasRole(userId, "Admin") || this.UserCanEditAccount(userId, accountId);

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
                var hasAccess = this.UserHasRole(userId, "Admin") || this.UserCanViewAccount(userId, accountId);

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
                                    Balance = accountWithBalance.Available,
                                    CurrencyId = accountWithBalance.Currency.CurrencyId,
                                    DisplayName = account.DisplayName,
                                    Type = account.AccountType
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

        public int CreateAccountHold(Guid sessionId, int accountId, decimal amount, string holdReason,
            DateTime? expiresAt)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }


                return context.CreateAccountHold(accountId, amount, holdReason, expiresAt);
            }
        }

        public void ReleaseTransactionHoldAndDebit(Guid sessionId, int accountHoldId, string specialAccountName)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }


                using (var scope = new TransactionScope())
                {
                    var accountHold = context.AccountHolds.Find(accountHoldId);
                    var destAccountId = context.GetSpecialAccountId(specialAccountName);
                    var sourceAccountId = accountHold.AccountId;
                    
                    context.ReleaseAccountHold(accountHoldId);
                    context.CreateTransfer(sourceAccountId, destAccountId, "Debited", accountHold.Amount);
                    
                    scope.Complete();
                }
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

        public virtual bool UserCanDepositToAccount(int userId, int accountId)
        {
            using (var context = this.GetContext())
            {
                return context.Accounts.Find(accountId).AnyCanDeposit ||
                    context.Users.Find(userId).AccountAccess.Any(x => x.CanDeposit && x.AccountId == accountId);
            }
        }

        /// <summary>
        /// The get context.
        /// </summary>
        /// <returns>
        /// </returns>
        private IOliveContext GetContext()
        {
            Contract.Ensures(Contract.Result<IOliveContext>() != null);

            var context = this.Container.Resolve<IOliveContext>();

            return context;
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

        public string GetLastProcessedTransactionId(Guid sessionId)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }


                return context.GetLastProcessedTransactionId();
            }
        }

        public bool TransactionIsProcessed(Guid sessionId, string transactionId)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                return context.BitcoinTransactionIsProcessed(transactionId);
            }
        }

        public void CreditTransactionWithHold(Guid sessionId, int accountId, string transactionId, decimal amount, string currencyId)
        {
            var holdReason = "Hold for Bitcoin incoming transaction #" + transactionId;

            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                using (var scope = new TransactionScope())
                {
                    var sourceAccountId = context.GetSpecialAccountId("BitcoinSyncIncoming" + currencyId);

                    var destAccount = context.Accounts.FirstOrDefault(x => x.AccountId == accountId);

                    if (destAccount == null)
                    {
                        throw this.FaultFactory.CreateAccountNotFoundFaultException(accountId);
                    }

                    context.CreateTransfer(sourceAccountId, destAccount.AccountId, "Bitcoin " + transactionId, amount);
                    var accountHoldId = context.CreateAccountHold(accountId, amount, holdReason, default(DateTime?));
                    context.CreateTransaction(transactionId, accountId, accountHoldId, amount);

                    scope.Complete();
                }
            }
        }

        public void ReleaseTransactionHold(Guid sessionId, string transactionId)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                var transaction = context.BitcoinTransactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    throw new Exception(string.Format("The specified transaction, #{0} was not found.", transactionId));
                }

                context.ReleaseAccountHold(transaction.AccountHoldId.Value);
            }
        }

        public void SetAccountReceiveAddress(Guid sessionId, int accountId, string receiveAddress)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                context.SetAccountReceiveAddress(accountId, receiveAddress);
            }
        }

        public string GetAccountReceiveAddress(Guid sessionId, int accountId)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync") || this.UserHasRole(userId, "Admin")
                                || this.UserCanViewAccount(userId, accountId);

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                return context.GetAccountReceiveAddress(accountId);
            }
        }

        public int GetOrCreateBitcoinWithdrawAccount(Guid sessionId, string currencyId, string receiveAddress)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);

                using (var scope = new TransactionScope())
                {
                    var account =
                        context.Accounts.Where(
                            x =>
                            x.BitcoinWithdrawAccount != null && x.CurrencyId == currencyId
                            && x.BitcoinWithdrawAccount.ReceiveAddress == receiveAddress
                            && x.Users.Any(u => u.UserId == userId && u.CanDeposit)).FirstOrDefault();

                    if (account == null)
                    {
                        account = new Account
                            {
                                AccountType = "BitcoinWithdraw",
                                AnyCanDeposit = false,
                                CurrencyId = currencyId,
                                DisplayName = null,
                                BitcoinWithdrawAccount = new BitcoinWithdrawAccount
                                    {
                                        ReceiveAddress = receiveAddress
                                    },
                                Users =
                                    new List<AccountUser>
                                        {
                                            new AccountUser { UserId = userId, CanDeposit = true, CanWithdraw = false } 
                                        }
                            };

                        context.Accounts.Add(account);
                        context.SaveChanges();
                    }

                    scope.Complete();

                    return account.AccountId;
                }
            }
        }

        public List<int> GetAccountsWithoutReceiveAddress(Guid sessionId, string currencyId)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                return
                    context.Accounts.Where(
                        a =>
                        a.AccountType != "Special" && a.CurrencyId == currencyId
                        && a.BitcoinAccountReceiveAddress == null || a.BitcoinAccountReceiveAddress.ReceiveAddress == null).Select(a => a.AccountId).ToList();
            }
        }

        public List<GetWithdrawAccountsForProcessingAccount> GetWithdrawAccountsForProcessing(Guid sessionId, string currencyId)
        {
            using (var context = this.GetContext())
            {
                var userId = this.VerifySession(sessionId);
                var hasAccess = this.UserHasRole(userId, "BitcoinSync");

                if (!hasAccess)
                {
                    throw this.FaultFactory.CreateUnauthorizedFeatureAccessFaultException();
                }

                var q = from a in context.AccountsWithBalance
                        where a.BitcoinWithdrawAccount != null && a.CurrencyId == currencyId && a.Available > 0
                        select
                            new GetWithdrawAccountsForProcessingAccount
                                {
                                    AccountId = a.AccountId,
                                    Available = a.Available,
                                    ReceiveAddress = a.BitcoinWithdrawAccount.ReceiveAddress
                                };

                var result = q.ToList();
                return result;
            }
        }
    }
}
