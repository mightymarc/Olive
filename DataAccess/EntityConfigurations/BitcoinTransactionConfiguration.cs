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

    public class BitcoinTransactionConfiguration : EntityTypeConfiguration<BitcoinTransaction>
    {
        public BitcoinTransactionConfiguration()
        {
            this.ToTable("Transaction", "Bitcoin");
            this.HasKey(c => c.TransactionId);
        }
    }
}
