// -----------------------------------------------------------------------
// <copyright file="BitcoinTransaction.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BitcoinTransaction
    {
        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public int AccountId { get; set; }

        public int? AccountHoldId { get; set; }
    }
}
