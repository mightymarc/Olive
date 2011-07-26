using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Exchange
{
    public class CreateOrderViewModel
    {
        public decimal? Volume { get; set; }

        public int? SourceAccountId { get; set; }

        public int? DestAccountId { get; set; }

        public decimal? Price { get; set; }
    }
}