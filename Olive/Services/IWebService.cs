// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebService.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the IWebService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;

    [ServiceContract]
    [ContractClass(typeof(IWebServiceContract))]
    public interface IWebService
    {
        [OperationContract]
        int CreateCurrentAccount(Guid sessionId, string currencyId, string displayName);

        [OperationContract]
        Guid CreateSession(string email, string password);

        [OperationContract]
        long CreateTransfer(Guid sessionId, int sourceAccountId, int destAccountId, decimal amount, string description);

        [OperationContract]
        void CreateUser(string email, string password);

        [OperationContract]
        GetAccountAccount GetAccount(Guid sessionId, int accountId);

        [OperationContract]
        List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId);

        [OperationContract]
        AccountOverview GetAccounts(Guid sessionId);

        [OperationContract]
        List<string> GetCurrencies();

        [OperationContract]
        void EditCurrentAccount(Guid sessionId, int accountId, string displayName);
    }
}