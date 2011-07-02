namespace Olive.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class AccountOverviewAccount
    {
        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public string CurrencyShortName { get; set; }
    }
}