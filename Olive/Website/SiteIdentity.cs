// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteIdentity.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the SiteIdentity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website
{
    using System;
    using System.Security.Principal;

    public sealed class SiteIdentity : IIdentity
    {
        public SiteIdentity(Guid userId)
        {
            this.SessionId = userId;
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

        public string Name
        {
            get
            {
                return this.SessionId.ToString();
            }
        }

        public Guid SessionId { get; private set; }
    }
}