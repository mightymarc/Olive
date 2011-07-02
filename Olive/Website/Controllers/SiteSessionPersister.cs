// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteSessionPersister.cs" company="Olive">
//   Olive
// </copyright>
// <summary>
//   The site session persister.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Web;

    /// <summary>
    /// The site session persister.
    /// </summary>
    public class SiteSessionPersister : ISiteSessionPersister
    {
        /// <summary>
        /// The session id key.
        /// </summary>
        private const string SessionIdKey = "SessionId";

        /// <summary>
        /// Gets a value indicating whether HasSession.
        /// </summary>
        public bool HasSession
        {
            get
            {
                return HttpContext.Current.Session[SessionIdKey] != null;
            }
        }

        /// <summary>
        /// Gets or sets SessionId.
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