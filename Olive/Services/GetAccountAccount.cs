using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetAccountAccount
    {
        [DataMember]
        public string DisplayName { get; set; }
    }
}