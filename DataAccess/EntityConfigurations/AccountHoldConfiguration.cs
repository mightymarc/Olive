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

    public class AccountHoldConfiguration : EntityTypeConfiguration<AccountHold>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountHoldConfiguration"/> class.
        /// </summary>
        public AccountHoldConfiguration()
        {
            this.ToTable("AccountHold", "Banking");
            this.HasKey(m => m.AccountHoldId);

            this.HasRequired(m => m.Account).WithMany(m => m.Holds);
        }
    }
}
