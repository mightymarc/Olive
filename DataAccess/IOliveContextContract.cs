﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOliveContextContract.cs" company="Olive">
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
//   Defines the IOliveContextContract type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.Contracts;

    using Olive.DataAccess.Domain;

    /// <summary>
    /// The i olive context contract.
    /// </summary>
    [ContractClassFor(typeof(IOliveContext))]
    public abstract class IOliveContextContract : IOliveContext
    {
        /// <summary>
        ///   Gets or sets Accounts.
        /// </summary>
        public IDbSet<Account> Accounts
        {
            get
            {
                return default(IDbSet<Account>);
            }

            set
            {
                return;
            }
        }

        /// <summary>
        ///   Gets or sets AccountsWithBalance.
        /// </summary>
        public IDbSet<AccountWithBalance> AccountsWithBalance
        {
            get
            {
                return default(IDbSet<AccountWithBalance>);
            }

            set
            {
                return;
            }
        }

        public IDbSet<AccountHold> AccountHolds
        {
            get
            {
                return default(IDbSet<AccountHold>);
            }

            set
            {
                return;
            }
        
        }
        /// <summary>
        ///   Gets or sets Currencies.
        /// </summary>
        public IDbSet<Currency> Currencies
        {
            get
            {
                return default(IDbSet<Currency>);
            }

            set
            {
                return;
            }
        }

        public IDbSet<ExchangeOrder> ExchangeOrders
        {
            get
            {
                return default(IDbSet<ExchangeOrder>);
            }
            set
            {
                return;
            }
        }

        public IDbSet<ExchangeMarket> ExchangeMarkets { get; set; }

        public IDbSet<ExchangePrice> ExchangePrices
        {
            get
            {
                return default(IDbSet<ExchangePrice>);
            }
            set
            {
                return;
            }
        }

        /// <summary>
        ///   Gets or sets Sessions.
        /// </summary>
        public IDbSet<Session> Sessions
        {
            get
            {
                return default(IDbSet<Session>);
            }

            set
            {
                return;
            }
        }

        /// <summary>
        ///   Gets or sets Transfers.
        /// </summary>
        public IDbSet<Transfer> Transfers
        {
            get
            {
                return default(IDbSet<Transfer>);
            }

            set
            {
                return;
            }
        }

        /// <summary>
        ///   Gets or sets Users.
        /// </summary>
        public IDbSet<User> Users
        {
            get
            {
                return default(IDbSet<User>);
            }

            set
            {
                return;
            }
        }

        public IDbSet<BitcoinTransaction> BitcoinTransactions
        {
            get
            {
                return default(IDbSet<BitcoinTransaction>);
            }
            set
            {
                return;
            }
        }

        /// <summary>
        /// The create current account.
        /// </summary>
        /// <param name="userId">
        /// The user id.
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
        public int CreateCurrentAccount(int userId, string currencyId, string displayName)
        {
            Contract.Requires<ArgumentException>(userId > 0, "userId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(currencyId), "currencyId");
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        /// <summary>
        /// The create session.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="passwordHash">
        /// The password hash.
        /// </param>
        /// <returns>
        /// </returns>
        public Guid CreateSession(string email, string passwordHash)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(passwordHash), "passwordHash");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(email), "email");
            return Guid.Empty;
        }

        /// <summary>
        /// The create transfer.
        /// </summary>
        /// <param name="fromAccountId">
        /// The source account id.
        /// </param>
        /// <param name="toAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="volume">
        /// The volume.
        /// </param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        public long CreateTransfer(int fromAccountId, int toAccountId, string fromComment, string toComment, decimal volume)
        {
            Contract.Requires<ArgumentException>(fromAccountId > 0, "sourceAccount");
            Contract.Requires<ArgumentException>(toAccountId > 0, "toAccountId");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(fromComment), "fromComment");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(toComment), "destComment");
            Contract.Requires<ArgumentException>(volume > 0, "volume");
            Contract.Ensures(Contract.Result<long>() > 0);

            return default(long);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void EditCurrentAccount(int accountId, string displayName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        public void SaveChanges()
        {
            return;
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
        public int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            return default(int);
        }

        public string GetLastProcessedTransactionId()
        {
            return default(string);
        }

        public bool BitcoinTransactionIsProcessed(string transactionId)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(transactionId), "transactionId");
            return default(bool);
        }

        public int CreateAccountHold(int accountId, decimal amount, string holdReason, DateTime? expiresAt)
        {
            return default(int);
        }

        public void CreditTransaction(string transactionId, int accountId, int accountHoldId, decimal amount)
        {
            return;
        }

        public void ReleaseAccountHold(int accountHoldId)
        {
            return;
        }

        public int GetSpecialAccountId(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name), "name");
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        public void CreateTransaction(string transactionId, int accountId, int accountHoldId, decimal amount)
        {
            Contract.Requires<ArgumentNullException>(transactionId != null, "transactionId");
            Contract.Requires<ArgumentException>(transactionId.Length == 64, "transactionId length");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId > 0");
            Contract.Requires<ArgumentException>(accountHoldId > 0, "accountHoldId > 0");
            Contract.Requires<ArgumentException>(amount > 0, "volume > 0");
            return;
        }

        public void SetAccountReceiveAddress(int accountId, string receiveAddress)
        {
            Contract.Requires<ArgumentException>(accountId > 0, "accountId > 0");
            Contract.Requires<ArgumentNullException>(receiveAddress != null, "receiveAddress");
            Contract.Requires<ArgumentException>(receiveAddress.Length == 34, "receiveAddress length");
        }

        public string GetAccountReceiveAddress(int accountId)
        {
            Contract.Requires<ArgumentException>(accountId > 0, "accountId > 0");
            Contract.Ensures(Contract.Result<string>() == null || Contract.Result<string>().Length == 34, "receiveAddress");
            return default(string);
        }

        public List<ExchangePrice> GetPricesForMarket(int marketId)
        {
            Contract.Requires<ArgumentException>(marketId > 0, "marketId");
            Contract.Ensures(Contract.Result<List<ExchangePrice>>() != null);
            Contract.Ensures(Contract.ForAll(Contract.Result<List<ExchangePrice>>(), x => x != null));
            Contract.Ensures(Contract.ForAll(Contract.Result<List<ExchangePrice>>(), x => x.Volume > 0));
            Contract.Ensures(Contract.ForAll(Contract.Result<List<ExchangePrice>>(), x => x.Price > 0));
            return default(List<ExchangePrice>);
        }

        public int CreateOrder(int sourceAccountId, int destAccountId, decimal price, decimal volume)
        {
            Contract.Requires<ArgumentException>(sourceAccountId > 0, "fromAccountId");
            Contract.Requires<ArgumentException>(destAccountId > 0, "toAccountId");
            Contract.Requires<ArgumentException>(sourceAccountId != destAccountId, "fromAccountId != toAccountId");
            Contract.Requires<ArgumentException>(price > 0, "price");
            Contract.Requires<ArgumentException>(volume > 0, "volume");
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }
    }
}