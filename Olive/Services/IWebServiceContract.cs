// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebServiceContract.cs" company="Olive">
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
//   Defines the IWebServiceContract type.
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

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", 
        Justification = "Contract for IWebService.")]
    [ContractClassFor(typeof(IWebService))]
    public abstract class IWebServiceContract : IWebService
    {
        #region Public Methods

        public int CreateCurrentAccount(Guid sessionId, string currencyId, string displayName)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(currencyId), "currencyId");
            Contract.Ensures(Contract.Result<int>() > 0);

            return default(int);
        }

        public Guid CreateSession(string email, string password)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email), "email");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(password), "password");
            Contract.Requires<ArgumentException>(
                Regex.IsMatch(
                    email, 
                    @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"));

            ////Contract.Requires<ArgumentException>(Regex.IsMatch(password, "^.{8,50}$"));
            Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);

            return default(Guid);
        }

        public long CreateTransfer(
            Guid sessionId, int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(sourceAccountId > 0, "sourceAccountId");
            Contract.Requires<ArgumentException>(destAccountId > 0, "destAccountId");
            Contract.Requires<ArgumentException>(sourceAccountId != destAccountId, "sourceAccount != destAccount");
            Contract.Requires<ArgumentException>(amount > 0, "amount");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(description), "description");
            return default(long);
        }

        public void CreateUser(string email, string password)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email), "email");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(password), "password");
            Contract.Requires<ArgumentException>(
                Regex.IsMatch(
                    email, 
                    @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"));
            Contract.Requires<ArgumentException>(Regex.IsMatch(password, "^.{8,50}$"));
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

        #endregion
    }
}