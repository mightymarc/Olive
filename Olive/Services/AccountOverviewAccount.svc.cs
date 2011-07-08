// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountOverviewAccount.svc.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the AccountOverviewAccount type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class AccountOverviewAccount
    {
        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public string CurrencyId { get; set; }

        [DataMember]
        public string DisplayName { get; set; }
    }
}