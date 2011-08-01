using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Services
{
    public class GetMarketResponse
    {
        public int MarketId { get; set; }

        public string FromCurrencyId { get; set; }

        public string ToCurrencyId { get; set; }
    }

    public class GetMarketPricesResponse
    {
        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public int MarketId { get; set; }

        public List<GetMarketPricesResponsePrice> Prices { get; set; }
    }

    public class GetMarketPricesResponsePrice
    {
        public decimal Price { get; set; }

        public decimal Volume { get; set; }
    }
}
