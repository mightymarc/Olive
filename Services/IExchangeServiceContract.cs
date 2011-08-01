namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;

    [ContractClassFor(typeof(IExchangeService))]
    public abstract class IExchangeServiceContract : IExchangeService
    {
        public GetMarketResponse GetMarket(string fromCurrencyId, string toCurrencyId)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(fromCurrencyId), "fromCurrencyId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(toCurrencyId), "toCurrencyId");
            Contract.Ensures(Contract.Result<GetMarketResponse>() != null);
            Contract.Ensures(Contract.Result<GetMarketResponse>().MarketId > 0);
            Contract.Ensures(Contract.Result<GetMarketResponse>().FromCurrencyId == fromCurrencyId);
            Contract.Ensures(Contract.Result<GetMarketResponse>().ToCurrencyId == toCurrencyId);

            return default(GetMarketResponse);
        }

        public GetMarketPricesResponse GetMarketPrices(int marketId)
        {
            Contract.Requires<ArgumentException>(marketId > 0, "marketId");
            Contract.Ensures(Contract.Result<GetMarketPricesResponse>() != null);
            Contract.Ensures(Contract.Result<GetMarketPricesResponse>().Prices != null);
            Contract.Ensures(Contract.Result<GetMarketPricesResponse>().MarketId == marketId);
            Contract.Ensures(Contract.ForAll(Contract.Result<GetMarketPricesResponse>().Prices, x => x != null));
            Contract.Ensures(Contract.ForAll(Contract.Result<GetMarketPricesResponse>().Prices, x => x.Volume > 0));
            Contract.Ensures(Contract.ForAll(Contract.Result<GetMarketPricesResponse>().Prices, x => x.Price > 0));

            return default(GetMarketPricesResponse);
        }

        public int CreateOrder(Guid sessionId, int sourceAccountId, int destAccountId, decimal price, decimal volume)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(sourceAccountId > 0, "sourceAccountId");
            Contract.Requires<ArgumentException>(destAccountId > 0, "destAccountId");
            Contract.Requires<ArgumentException>(price > 0, "price");
            Contract.Requires<ArgumentException>(volume > 0, "volume");
            Contract.Ensures(Contract.Result<int>() > 0);

            return default(int);
        }

        public GetMarketResponse GetMarket(int marketId)
        {
            Contract.Requires<ArgumentException>(marketId > 0, "marketId");
            Contract.Ensures(Contract.Result<GetMarketResponse>() != null);
            Contract.Ensures(Contract.Result<GetMarketResponse>().MarketId == marketId);
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<GetMarketResponse>().FromCurrencyId));
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<GetMarketResponse>().ToCurrencyId));

            return default(GetMarketResponse);
        }

        public List<GetMarketResponse> GetMarkets()
        {
            Contract.Ensures(Contract.Result<List<GetMarketResponse>>() != null);
            Contract.Ensures(Contract.ForAll(Contract.Result<List<GetMarketResponse>>(), m => m.MarketId > 0));
            Contract.Ensures(Contract.ForAll(Contract.Result<List<GetMarketResponse>>(), m => !string.IsNullOrEmpty(m.FromCurrencyId)));
            Contract.Ensures(Contract.ForAll(Contract.Result<List<GetMarketResponse>>(), m => !string.IsNullOrEmpty(m.ToCurrencyId)));

            return default(List<GetMarketResponse>);
        }

        public List<GetMarketPricesResponse> GetAllMarketPrices()
        {
            throw new NotImplementedException();
        }
    }
}