namespace Olive.DataAccess
{
    using System;

    public class Transfer
    {
        public long TransferId { get; set; }

        public string Description { get; set; }

        public int SourceAccountId { get; set; }

        public int DestAccountId { get; set; }

        public Account SourceAccount { get; set; }

        public Account DestAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}