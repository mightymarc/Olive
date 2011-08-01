// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBankService.cs" company="Olive">
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
//   Defines the IBankService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;

    /// <summary>
    /// The i web service.
    /// </summary>
    [ServiceContract]
    [ContractClass(typeof(IBankServiceContract))]
    public interface IBankService
    {
        /// <summary>
        /// The create current account.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="currencyId">The specialAccountName id.</param>
        /// <param name="displayName">The display name.</param>
        /// <returns>
        /// The create current account.
        /// </returns>
        [OperationContract]
        int CreateCurrentAccount(Guid sessionId, string currencyId, string displayName);

        [OperationContract]
        int GetSpecialAccountId(string name);

        /// <summary>
        /// The create transfer.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="fromAccountId">The source account id.</param>
        /// <param name="toAccountId">The dest account id.</param>
        /// <param name="volume">The volume.</param>
        /// <param name="fromComment">The fromComment.</param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        [OperationContract]
        long CreateTransfer(Guid sessionId, int fromAccountId, int toAccountId, decimal volume, string fromComment, string toComment);

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="displayName">The display name.</param>
        [OperationContract]
        void EditCurrentAccount(Guid sessionId, int accountId, string displayName);

        /// <summary>
        /// The get account.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        [OperationContract]
        GetAccountAccount GetAccount(Guid sessionId, int accountId);

        /// <summary>
        /// The get account transfers.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        [OperationContract]
        List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId);

        /// <summary>
        /// The get accounts.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <returns></returns>
        [OperationContract]
        AccountOverview GetAccounts(Guid sessionId);

        /// <summary>
        /// The get currencies.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetCurrencies();

        [OperationContract]
        int CreateAccountHold(Guid sessionId, int accountId, decimal amount, string holdReason, DateTime? expiresAt);

        [OperationContract]
        void ReleaseTransactionHoldAndDebit(Guid sessionId, int accountHoldId, string specialAccountName);
    }
}