// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteSession.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the SiteSession type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Helpers
{
    using System;
    using System.Web;

    /// <summary>
    ///   The site session persister.
    /// </summary>
    public class SiteSession : ISiteSession
    {
        /// <summary>
        ///   The session id key.
        /// </summary>
        private const string SessionIdKey = "SessionId";

        /// <summary>
        ///   Gets a value indicating whether there is a session.
        /// </summary>
        public bool HasSession
        {
            get
            {
                return HttpContext.Current.Session[SessionIdKey] != null;
            }
        }

        /// <summary>
        ///   Gets or sets the session id.
        /// </summary>
        public Guid SessionId
        {
            get
            {
                return HttpContext.Current.Session[SessionIdKey] == null
                           ? Guid.Empty
                           : (Guid)HttpContext.Current.Session[SessionIdKey];
            }

            set
            {
                HttpContext.Current.Session[SessionIdKey] = value;
            }
        }
    }
}