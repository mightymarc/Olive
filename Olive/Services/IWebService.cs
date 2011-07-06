namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    [ServiceContract]
    [ContractClass(typeof(IWebServiceContract))]
    public interface IWebService
    {
        [OperationContract]
        [FaultContract(typeof(AuthenticationFault))]
        Guid CreateSession(string email, string password);

        [OperationContract]
        void CreateUser(string email, string password);

        [OperationContract]
        List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId);

        [OperationContract]
        GetAccountAccount GetAccount(Guid sessionId, int accountId);

        [OperationContract]
        AccountOverview GetAccounts(Guid sessionId);

        [OperationContract]
        int CreateAccount(Guid sessionId, string currencyId, string displayName);

        [OperationContract]
        void EditAccount(Guid sessionId, int accountId, string displayName);
    }
}