namespace Olive.DataAccess
{
    using System.Collections.Generic;

    public class User
    {
        public int UserId { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<AccountUser> AccountAccess { get; set; }

        public string PasswordSalt { get; set; }
    }
}