// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOliveContext.cs" company="Olive">
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
//   Defines the IOliveContext type.
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
    /// The i olive context.
    /// </summary>
    [ContractClass(typeof(IOliveContextContract))]
    public interface IOliveContext : IDisposable
    {
        /// <summary>
        ///   Gets or sets Accounts.
        /// </summary>
        IDbSet<Account> Accounts { get; set; }

        IDbSet<AccountHold> AccountHolds { get; set; }

        /// <summary>
        ///   Gets or sets AccountsWithBalance.
        /// </summary>
        IDbSet<AccountWithBalance> AccountsWithBalance { get; set; }

        /// <summary>
        ///   Gets or sets Currencies.
        /// </summary>
        IDbSet<Currency> Currencies { get; set; }

        IDbSet<ExchangeOrder> ExchangeOrders { get; set; }

        IDbSet<ExchangeMarket> ExchangeMarkets { get; set; }

        IDbSet<ExchangePrice> ExchangePrices { get; set; }

        /// <summary>
        ///   Gets or sets Sessions.
        /// </summary>
        IDbSet<Session> Sessions { get; set; }

        /// <summary>
        ///   Gets or sets Transfers.
        /// </summary>
        IDbSet<Transfer> Transfers { get; set; }

        /// <summary>
        ///   Gets or sets Users.
        /// </summary>
        IDbSet<User> Users { get; set; }

        IDbSet<BitcoinTransaction> BitcoinTransactions { get; set; }

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
        int CreateCurrentAccount(int userId, string currencyId, string displayName);

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
        Guid CreateSession(string email, string passwordHash);

        /// <summary>
        /// Creates the transfer.
        /// </summary>
        /// <param name="sourceAccountId">
        /// The source account id.
        /// </param>
        /// <param name="destAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount);

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        void EditCurrentAccount(int accountId, string displayName);

        /// <summary>
        /// The save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Verifies that specified session exists and is not expired.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The user id of the user that owns the session.
        /// </returns>
        int VerifySession(Guid sessionId);

        /// <summary>
        /// Gets the last processed transaction id.
        /// </summary>
        /// <returns></returns>
        string GetLastProcessedTransactionId();

        bool BitcoinTransactionIsProcessed(string transactionId);

        /// <summary>
        /// Creates the account hold.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="holdReason">The hold reason.</param>
        /// <param name="expiresAt">The expires at.</param>
        /// <returns></returns>
        int CreateAccountHold(int accountId, decimal amount, string holdReason, DateTime? expiresAt);

        void ReleaseAccountHold(int accountHoldId);

        int GetSpecialAccountId(string name);

        void CreateTransaction(string transactionId, int accountId, int accountHoldId, decimal amount);

        void SetAccountReceiveAddress(int accountId, string receiveAddress);

        string GetAccountReceiveAddress(int accountId);

        int CreateOrder(int sourceAccountId, int destAccountId, decimal price, decimal volume);
    }
}