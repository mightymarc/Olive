// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISiteSession.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the ISiteSession type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Helpers
{
    using System;

    /// <summary>
    ///   Helper class to access session information.
    /// </summary>
    public interface ISiteSession
    {
        /// <summary>
        ///   Gets a value indicating whether there is a session.
        /// </summary>
        bool HasSession { get; }

        /// <summary>
        ///   Gets or sets the session id.
        /// </summary>
        Guid SessionId { get; set; }
    }
}