namespace Olive.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetWithdrawAccountsForProcessingAccount
    {
        [DataMember]
        public decimal Available { get; set; }

        [DataMember]
        public string ReceiveAddress { get; set; }

        [DataMember]
        public int AccountId { get; set; }
    }
}