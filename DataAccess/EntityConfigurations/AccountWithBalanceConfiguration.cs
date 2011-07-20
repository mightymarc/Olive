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

    public class AccountWithBalanceConfiguration : EntityTypeConfiguration<AccountWithBalance>
    {
        public AccountWithBalanceConfiguration()
        {
            this.ToTable("AccountWithBalance", "Banking");
        }
    }
}
