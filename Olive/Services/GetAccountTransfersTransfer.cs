// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAccountTransfersTransfer.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the GetAccountTransfersTransfer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class GetAccountTransfersTransfer
    {
        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string CurrencyShortName { get; set; }

        [DataMember]
        public int DestAccountId { get; set; }

        [DataMember]
        public int SourceAccountId { get; set; }

        [DataMember]
        public Guid TransferId { get; set; }
    }
}