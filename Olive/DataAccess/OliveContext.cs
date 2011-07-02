// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveContext.cs" company="Olive">
//   Copyright © Olive 2011
// </copyright>
// <summary>
//   Defines the OliveContext data access class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The entity framework database context.
    /// </summary>
    public class OliveContext : DbContext
    {
        // private string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="OliveContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public OliveContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Gets or sets Accounts.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets AccountsWithBalance.
        /// </summary>
        public DbSet<AccountWithBalance> AccountsWithBalance { get; set; }

        /// <summary>
        /// Gets or sets Currencies.
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets Sessions.
        /// </summary>
        public DbSet<Session> Sessions { get; set; }

        /// <summary>
        /// Gets or sets Transfers.
        /// </summary>
        public DbSet<Transfer> Transfers { get; set; }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Creates the session.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns></returns>
        public Guid CreateSession(int userId, string passwordHash)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(passwordHash), "passwordHash");
            Contract.Requires<ArgumentException>(userId > 0, "userId");
            Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);

            using (var command = this.Database.Connection.CreateCommand("Auth.CreateSession"))
            {
                command.AddParam("@UserId", DbType.Int32, userId);
                command.AddParam("@PasswordHash", DbType.String, passwordHash, size: 100);
                command.AddParam("@SessionId", DbType.Guid, direction: ParameterDirection.Output);

                switch (command.ExecuteCommand())
                {
                    case 0:
                        var sessionId = (Guid)command.Parameters["@SessionId"].Value;
                        Contract.Assume(sessionId != Guid.Empty);
                        return sessionId;
                    default:
                        throw new UnknownReturnCodeException(command.GetReturnCode());
                }
            }
        }


        /// <summary>
        /// Creates the transfer.
        /// </summary>
        /// <param name="sourceAccountId">The source account id.</param>
        /// <param name="destAccountId">The dest account id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The identifier for the transfer that was created.</returns>
        public long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            Contract.Requires<ArgumentException>(sourceAccountId > 0, "sourceAccount");
            Contract.Requires<ArgumentException>(destAccountId > 0, "destAccountId");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(description), "description");
            Contract.Requires<ArgumentException>(amount > 0, "amount");
            Contract.Ensures(Contract.Result<long>() > 0);

            using (var command = this.Database.Connection.CreateCommand("Banking.CreateTransfer"))
            {
                command.AddParam("@SourceAccountId", DbType.Int32, sourceAccountId);
                command.AddParam("@DestAccountId", DbType.Int32, destAccountId);
                command.AddParam("@DestAccountId", DbType.String, description);
                command.AddParam("@Amount", DbType.Decimal, amount);
                command.AddParam("@Description", DbType.String, description);
                command.AddParam("@TransferId", DbType.Int64, direction: ParameterDirection.Output);

                switch (command.ExecuteCommand())
                {
                    case 0:
                        var transactionId = (long)command.Parameters["@TransferId"].Value;
                        Contract.Assume(transactionId > 0);
                        return transactionId;
                    default:
                        throw new UnknownReturnCodeException(command.GetReturnCode());
                }
            }
        }

        /// <summary>
        /// Verifies that specified session exists and is not expired.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <returns>
        /// The user id of the user that owns the session.
        /// </returns>
        public int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            using (var command = this.Database.Connection.CreateCommand("Auth.VerifySession"))
            {
                command.AddParam("@UserId", DbType.Int32, direction: ParameterDirection.Output);
                command.AddParam("@SessionId", DbType.Guid, sessionId);

                switch (command.ExecuteCommand())
                {
                    case 0:
                        var userId = (int)command.Parameters["@UserId"].Value;
                        Contract.Assume(userId > 0);
                        return userId;
                    case 100:
                        throw new SessionDoesNotExistException();
                    default:
                        throw new UnknownReturnCodeException(command.GetReturnCode());
                }
            }
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
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
            modelBuilder.Entity<Transfer>().HasRequired(t => t.SourceAccount).WithMany(m => m.OutgoingTransfers).HasForeignKey(m => m.SourceAccountId);
            modelBuilder.Entity<Transfer>().HasRequired(t => t.DestAccount).WithMany(m => m.IncomingTransfers).HasForeignKey(m => m.DestAccountId);

            // dbo.User
            modelBuilder.Entity<User>().ToTable("User", "dbo");

            modelBuilder.Entity<Session>().ToTable("Session", "Auth");
            modelBuilder.Entity<Currency>().ToTable("Currency", "dbo");

            // Banking.Account
            modelBuilder.Entity<Account>().ToTable("Account", "Banking");
            modelBuilder.Entity<Account>().Property(m => m.AccountType).HasColumnName("Type");

            modelBuilder.Entity<AccountWithBalance>().ToTable("AccountWithBalance", "Banking");
        }
    }
}