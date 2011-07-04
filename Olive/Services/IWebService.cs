namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    [ServiceContract]
    public interface IWebService
    {
        [OperationContract]
        [FaultContract(typeof(AuthenticationFault))]
        Guid CreateSession(string email, string password);

        [OperationContract]
        int CreateUser(string email, string password);

        [OperationContract]
        AccountOverview GetAccounts(Guid sessionId);

        [OperationContract]
        List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId);

        [OperationContract]
        GetAccountAccount GetAccount(Guid sessionId, int accountId);

        [OperationContract]
        AccountOverview GetAccountOverview(Guid sessionId);

        [OperationContract]
        int CreateAccount(Guid mockSessionId, int currencyId, string displayName);
    }
}
