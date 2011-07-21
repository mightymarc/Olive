// -----------------------------------------------------------------------
// <copyright file="AccountHold.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AccountHold
    {
        public int AccountHoldId { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public string Reason { get; set; }
    }
}
