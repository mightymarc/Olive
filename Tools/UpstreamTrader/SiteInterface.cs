// -----------------------------------------------------------------------
// <copyright file="SiteInterface.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.UpstreamTrader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class SiteInterface
    {
        protected SiteConfiguration SiteConfiguration { get; private set; }

        public SiteInterface(SiteConfiguration siteConfiguration)
        {
            this.SiteConfiguration = siteConfiguration;
        }

        public abstract List<Market> GetMarkets();

        public abstract List<MarketPrice> GetPrices(string marketKey);

        public abstract List<Price> GetPrices();
    }
}
