// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveContext.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the OliveContext data access class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///   The entity framework database context.
    /// </summary>
    public partial class OliveContext : DbContext, IOliveContext
    {
        /// <summary>
        ///   The connection used to execute stored procedures.
        /// </summary>
        private IDbConnection commandConnection;

        [InjectionConstructor]
        public OliveContext()
            : base("OliveConnectionString")
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "OliveContext" /> class.
        /// </summary>
        /// <param name = "nameOrConnectionString">The name or connection string.</param>
        public OliveContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        ///   Gets or sets Accounts.
        /// </summary>
        public IDbSet<Account> Accounts { get; set; }

        /// <summary>
        ///   Gets or sets AccountsWithBalance.
        /// </summary>
        public IDbSet<AccountWithBalance> AccountsWithBalance { get; set; }

        /// <summary>
        ///   Gets or sets the connection used to execute stored procedures.
        /// </summary>
        /// <value>
        ///   The command connection.
        /// </value>
        public virtual IDbConnection CommandConnection
        {
            get
            {
                return this.commandConnection ?? (this.commandConnection = this.Database.Connection);
            }

            set
            {
                this.commandConnection = value;
            }
        }

        /// <summary>
        ///   Gets or sets Currencies.
        /// </summary>
        public IDbSet<Currency> Currencies { get; set; }

        /// <summary>
        ///   Gets or sets Sessions.
        /// </summary>
        public IDbSet<Session> Sessions { get; set; }

        /// <summary>
        ///   Gets or sets Transfers.
        /// </summary>
        public IDbSet<Transfer> Transfers { get; set; }

        /// <summary>
        ///   Gets or sets Users.
        /// </summary>
        public IDbSet<User> Users { get; set; }

        /// <summary>
        ///   Saves the changes.
        /// </summary>
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        /// <summary>
        ///   This method is called when the model for a derived context has been initialized, but
        ///   before the model has been locked down and used to initialize the context.  The default
        ///   implementation of this method does nothing, but it can be overridden in a derived class
        ///   such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name = "modelBuilder">The builder that defines the model for the context being created.</param>
        [SuppressMessage("Microsoft.Contracts", "Requires", Justification = "Warning from framework class.")]
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // dbo.AccountUser
            modelBuilder.Entity<AccountUser>().ToTable("AccountUser", "Banking");
            modelBuilder.Entity<AccountUser>().HasKey(e => new { e.AccountId, e.UserId });

            // AccountUser.UserId -> User.UserId
            modelBuilder.Entity<AccountUser>().HasRequired(m => m.User).WithMany(m => m.AccountAccess).HasForeignKey(
                m => m.UserId);

            // AccountUser.AccountId -> Account.AccountId
            modelBuilder.Entity<AccountUser>().HasRequired(m => m.Account).WithMany(m => m.Users).HasForeignKey(
                m => m.AccountId);

            // Banking.Transfer
            modelBuilder.Entity<Transfer>().ToTable("Transfer", "Banking");
            modelBuilder.Entity<Transfer>().HasRequired(t => t.SourceAccount).WithMany(m => m.OutgoingTransfers).
                HasForeignKey(m => m.SourceAccountId);
            modelBuilder.Entity<Transfer>().HasRequired(t => t.DestAccount).WithMany(m => m.IncomingTransfers).
                HasForeignKey(m => m.DestAccountId);

            // dbo.User
            modelBuilder.Entity<User>().ToTable("User", "dbo");

            modelBuilder.Entity<Session>().ToTable("Session", "Auth");

            // dbo.Currency
            modelBuilder.Entity<Currency>().ToTable("Currency", "dbo");
            modelBuilder.Entity<Currency>().HasKey(c => c.CurrencyId);

            // Banking.Account
            modelBuilder.Entity<Account>().ToTable("Account", "Banking");
            modelBuilder.Entity<Account>().Property(m => m.AccountType).HasColumnName("Type");

            modelBuilder.Entity<AccountWithBalance>().ToTable("AccountWithBalance", "Banking");
        }
    }
}