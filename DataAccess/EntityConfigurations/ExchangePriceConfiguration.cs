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

    public class ExchangePriceConfiguration : EntityTypeConfiguration<ExchangePrice>
    {
        public ExchangePriceConfiguration()
        {
            this.ToTable("Price", "Exchange");
            this.HasKey(m => m.MarketId);
        }
    }
}
