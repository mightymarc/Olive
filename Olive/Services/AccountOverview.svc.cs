namespace Olive.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class AccountOverview : List<AccountOverviewAccount>
    {
    }
}