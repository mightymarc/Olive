// -----------------------------------------------------------------------
// <copyright file="ExchangeMarket.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ExchangeMarket
    {
        public int MarketId { get; set; }

        public string FromCurrencyId { get; set; }

        public string ToCurrencyId { get; set; }
    }
}
