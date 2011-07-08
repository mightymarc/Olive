// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Session.cs" company="Olive">
//   
// </copyright>
// <summary>
//   The session.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;

    /// <summary>
    ///   The session.
    /// </summary>
    public class Session
    {
        /// <summary>
        ///   Gets or sets CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///   Gets or sets ExpiresAt.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        ///   Gets or sets SessionId.
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        ///   Gets or sets User.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///   Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }
    }
}