namespace Olive.DataAccess
{
    public class AccountUser
    {
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool CanDeposit { get; set; }

        public bool CanWithdraw { get; set; }
    }
}