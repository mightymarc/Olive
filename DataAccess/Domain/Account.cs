﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Account.cs" company="Olive">
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
//   The account.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the holds.
        /// </summary>
        /// <value>
        /// The holds.
        /// </value>
        public virtual ICollection<AccountHold> Holds { get; set; }

        /// <summary>
        ///   Gets or sets AccountId.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        ///   Gets or sets the type of the account. Valid account types are defined in the AccountType class.
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        ///   Gets or sets Currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        ///   Gets or sets CurrencyId.
        /// </summary>
        public string CurrencyId { get; set; }

        /// <summary>
        ///   Gets or sets DisplayName.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///   Gets or sets IncomingTransfers.
        /// </summary>
        public virtual ICollection<Transfer> IncomingTransfers { get; set; }

        /// <summary>
        /// Gets or sets the bitcoin account receive address.
        /// </summary>
        /// <value>
        /// The bitcoin account receive address.
        /// </value>
        public virtual BitcoinAccountReceiveAddress BitcoinAccountReceiveAddress { get; set; }

        /// <summary>
        ///   Gets or sets OutgoingTransfers.
        /// </summary>
        public virtual ICollection<Transfer> OutgoingTransfers { get; set; }

        /// <summary>
        ///   Gets or sets Users.
        /// </summary>
        public virtual ICollection<AccountUser> Users { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether any user can deposit to this account.
        /// </summary>
        /// <value>
        ///   <c>true</c> if any user can deposit; otherwise, <c>false</c>.
        /// </value>
        public bool AnyCanDeposit { get; set; }

        public virtual BitcoinWithdrawAccount BitcoinWithdrawAccount { get; set; }
    }
}