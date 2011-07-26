namespace Olive.Services
{
    using System;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;

    [ContractClass(typeof(IExchangeServiceContract))]
    public interface IExchangeService
    {
        [OperationContract]
        GetMarketResponse GetMarket(string fromCurrencyId, string toCurrencyId);

        [OperationContract]
        GetMarketPricesResponse GetMarketPrices(int marketId);

        [OperationContract]
        int CreateOrder(Guid sessionId, int sourceAccountId, int destAccountId, decimal price, decimal volume);

        [OperationContract]
        GetMarketResponse GetMarket(int marketId);
    }
}