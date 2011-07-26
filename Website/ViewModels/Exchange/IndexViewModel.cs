using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Exchange
{
    using Olive.Services;

    public class IndexViewModel
    {
        public List<Tuple<GetMarketResponse, GetMarketPricesResponse>> Markets { get; set; }
    }
}