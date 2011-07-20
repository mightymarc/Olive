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

    public class AccountUserConfiguration : EntityTypeConfiguration<AccountUser>
    {
        public AccountUserConfiguration()
        {
            this.ToTable("AccountUser", "Banking");
            this.HasKey(m => new { m.AccountId, m.UserId });
            this.HasRequired(m => m.User).WithMany(m => m.AccountAccess).HasForeignKey(m => m.UserId);
            this.HasRequired(m => m.Account).WithMany(m => m.Users).HasForeignKey(m => m.AccountId);
        }
    }
}
