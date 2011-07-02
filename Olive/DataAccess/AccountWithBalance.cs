// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountWithBalance.cs" company="">
//   
// </copyright>
// <summary>
//   The account with balance.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    /// <summary>
    /// The account with balance.
    /// </summary>
    public class AccountWithBalance : Account
    {
        /// <summary>
        /// Gets or sets Balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}