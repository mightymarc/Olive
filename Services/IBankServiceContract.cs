// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBankServiceContract.cs" company="Olive">
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
//   Defines the IBankServiceContract type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Text.RegularExpressions;

    using Olive.DataAccess;
    using Olive.DataAccess.Domain;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
        Justification = "Contract for IBankService.")]
    [ContractClassFor(typeof(IBankService))]
    public abstract class IBankServiceContract : IBankService
    {
        public int CreateCurrentAccount(Guid sessionId, string currencyId, string displayName)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(currencyId), "currencyId");
            Contract.Ensures(Contract.Result<int>() > 0);

            return default(int);
        }

        public int GetSpecialAccountId(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name), "name");
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        public long CreateTransfer(
            Guid sessionId, int fromAccountId, int toAccountId, decimal volume, string fromComment, string toComment)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(fromAccountId > 0, "fromAccountId");
            Contract.Requires<ArgumentException>(toAccountId > 0, "toAccountId");
            Contract.Requires<ArgumentException>(fromAccountId != toAccountId, "sourceAccount != destAccount");
            Contract.Requires<ArgumentException>(volume > 0, "volume");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(fromComment), "fromComment");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(toComment), "toComment");
            Contract.Ensures(Contract.Result<long>() > 0);

            return default(long);
        }

        public void EditCurrentAccount(Guid sessionId, int accountId, string displayName)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");
            Contract.Requires<ArgumentException>(displayName != string.Empty, "displayName");
        }

        public GetAccountAccount GetAccount(Guid sessionId, int accountId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");
            Contract.Ensures(Contract.Result<GetAccountAccount>() != null);

            return default(GetAccountAccount);
        }

        public List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");
            Contract.Ensures(Contract.Result<List<GetAccountTransfersTransfer>>() != null);

            return default(List<GetAccountTransfersTransfer>);
        }

        public AccountOverview GetAccounts(Guid sessionId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<List<AccountWithBalance>>() != null);

            return default(AccountOverview);
        }

        public List<string> GetCurrencies()
        {
            Contract.Ensures(Contract.Result<List<string>>() != null);
            return default(List<string>);
        }

        public int CreateAccountHold(Guid sessionId, int accountId, decimal amount, string holdReason, DateTime? expiresAt)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(holdReason), "holdReason");
            Contract.Requires<ArgumentException>(amount > 0, "volume > 0");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId > 0");
            Contract.Requires<ArgumentException>(expiresAt == null || expiresAt.Value > DateTime.UtcNow);
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        public void ReleaseTransactionHoldAndDebit(Guid sessionId, int accountHoldId, string specialAccountName)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountHoldId > 0, "accountHoldId > 0");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(specialAccountName), "specialAccountName");
        }
    }
}