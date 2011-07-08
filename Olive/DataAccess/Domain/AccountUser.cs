// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountUser.cs" company="Olive">
//   
// </copyright>
// <summary>
//   The account user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    /// <summary>
    ///   The account user.
    /// </summary>
    public class AccountUser
    {
        /// <summary>
        ///   Gets or sets the account.
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        ///   Gets or sets the account id.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether the user may deposit into the account..
        /// </summary>
        public bool CanDeposit { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether the user may withdraw from the account..
        /// </summary>
        public bool CanWithdraw { get; set; }

        /// <summary>
        ///   Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///   Gets or sets user's id.
        /// </summary>
        public int UserId { get; set; }
    }
}