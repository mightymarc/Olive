// -----------------------------------------------------------------------
// <copyright file="AccountConfiguration.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.EntityConfigurations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    using Olive.DataAccess.Domain;

    public class ExchangeMarketConfiguration : EntityTypeConfiguration<ExchangeMarket>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeMarketConfiguration"/> class.
        /// </summary>
        public ExchangeMarketConfiguration()
        {
            this.ToTable("Market", "Exchange");
            this.HasKey(m => m.MarketId);
        }
    }
}
