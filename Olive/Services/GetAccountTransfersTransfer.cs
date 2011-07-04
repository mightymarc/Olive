using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetAccountTransfersTransfer
    {
        [DataMember]
        public string CurrencyShortName { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public int SourceAccountId { get; set; }

        [DataMember]
        public Guid TransferId { get; set; }

        [DataMember]
        public int DestAccountId { get; set; }
    }
}