// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Olive">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System.Collections.Generic;

    /// <summary>
    ///   The user.
    /// </summary>
    public class User
    {
        /// <summary>
        ///   Gets or sets AccountAccess.
        /// </summary>
        public virtual ICollection<AccountUser> AccountAccess { get; set; }

        /// <summary>
        ///   Gets or sets the email.
        /// </summary>
        /// <value>
        ///   The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        ///   Gets or sets PasswordHash.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        ///   Gets or sets PasswordSalt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        ///   Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }
    }
}