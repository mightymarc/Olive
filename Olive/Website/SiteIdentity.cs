using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website
{
    using System.Globalization;
    using System.Security.Principal;

    public sealed class SiteIdentity : IIdentity
    {
        public Guid SessionId { get; private set; }

        public SiteIdentity(Guid userId)
        {
            this.SessionId = userId;
        }

        public string Name
        {
            get
            {
                return this.SessionId.ToString();
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "Custom";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.SessionId != default(Guid);
            }
        }
    }
}