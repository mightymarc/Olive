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

    public class ExchangeOrderConfiguration : EntityTypeConfiguration<ExchangeOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeOrderConfiguration"/> class.
        /// </summary>
        public ExchangeOrderConfiguration()
        {
            this.ToTable("Order", "Exchange");
            this.HasKey(m => m.OrderId);
        }
    }
}
