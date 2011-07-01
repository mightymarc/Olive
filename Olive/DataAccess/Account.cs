namespace Olive.DataAccess
{
    using System.Collections.Generic;

    public class Account
    {
        public int AccountId { get; set; }

        public string DisplayName { get; set; }

        public AccountType Type { get; set; }

        public virtual ICollection<AccountUser> Users { get; set; }

        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
    }
}