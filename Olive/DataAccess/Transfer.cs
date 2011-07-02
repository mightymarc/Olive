// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transfer.cs" company="">
//   
// </copyright>
// <summary>
//   The transfer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;

    /// <summary>
    /// The transfer.
    /// </summary>
    public class Transfer
    {
        /// <summary>
        /// Gets or sets Amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets DestAccount.
        /// </summary>
        public virtual Account DestAccount { get; set; }

        /// <summary>
        /// Gets or sets DestAccountId.
        /// </summary>
        public int DestAccountId { get; set; }

        /// <summary>
        /// Gets or sets SourceAccount.
        /// </summary>
        public virtual Account SourceAccount { get; set; }

        /// <summary>
        /// Gets or sets SourceAccountId.
        /// </summary>
        public int SourceAccountId { get; set; }

        /// <summary>
        /// Gets or sets TransferId.
        /// </summary>
        public long TransferId { get; set; }
    }
}