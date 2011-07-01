using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website
{
    using System.Security.Principal;

    public class SitePrincipal : IPrincipal
    {
        public SitePrincipal(SiteIdentity identity)
        {
            this.Identity = identity;
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException("IsInRole is not implemented.");
        }

        public IIdentity Identity { get; private set; }
    }
}