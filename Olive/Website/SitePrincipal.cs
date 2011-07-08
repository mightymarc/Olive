// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SitePrincipal.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the SitePrincipal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website
{
    using System;
    using System.Security.Principal;

    public class SitePrincipal : IPrincipal
    {
        public SitePrincipal(SiteIdentity identity)
        {
            this.Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException("IsInRole is not implemented.");
        }
    }
}