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

    public class TransferConfiguration : EntityTypeConfiguration<Transfer>
    {
        public TransferConfiguration()
        {
            this.ToTable("Transfer", "Banking");
            this.HasRequired(t => t.SourceAccount).WithMany(m => m.OutgoingTransfers).HasForeignKey(m => m.SourceAccountId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.DestAccount).WithMany(m => m.IncomingTransfers).HasForeignKey(m => m.DestAccountId).WillCascadeOnDelete(false);
        }
    }
}
