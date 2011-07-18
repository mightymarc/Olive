namespace Olive.Bitcoin
{
    using System;

    public class Transaction
    {
        public string Account { get; set; }

        public string Address { get; set; }

        public string Category { get; set; }

        public decimal Amount { get; set; }

        public int? Confirmations { get; set; }

        public string TransactionId { get; set; }

        public DateTime Time { get; set; }
    }
}