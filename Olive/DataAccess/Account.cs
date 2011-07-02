// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Account.cs" company="Copyright">
//   [Copyright]
// </copyright>
// <summary>
//   The account.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System.Collections.Generic;

    /// <summary>
    /// The account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets AccountId.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets Currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets CurrencyId.
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets DisplayName.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets IncomingTransfers.
        /// </summary>
        public virtual ICollection<Transfer> IncomingTransfers { get; set; }

        /// <summary>
        /// Gets or sets OutgoingTransfers.
        /// </summary>
        public virtual ICollection<Transfer> OutgoingTransfers { get; set; }

        /// <summary>
        /// Gets or sets the type of the account. Valid account types are defined in the AccountType class.
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        public virtual ICollection<AccountUser> Users { get; set; }
    }
}