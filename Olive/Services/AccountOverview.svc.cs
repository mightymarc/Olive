// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountOverview.svc.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the AccountOverview type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class AccountOverview : List<AccountOverviewAccount>
    {
    }
}