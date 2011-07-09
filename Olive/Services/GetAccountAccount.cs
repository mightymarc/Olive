// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAccountAccount.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the GetAccountAccount type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class GetAccountAccount
    {
        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public string CurrencyId { get; set; }

        [DataMember]
        public string AccountType { get; set; }
    }
}