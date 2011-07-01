// -----------------------------------------------------------------------
// <copyright file="OliveContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class OliveContext : DbContext
    {
        //private string connectionString;

        public DbSet<AccountWithBalance> AccountsWithBalance { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Transfer> Transfers { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public OliveContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<AccountUser>().HasKey(e => new { e.AccountId, e.UserId });
            modelBuilder.Entity<Session>().ToTable("Session", "Auth");
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Currency>().ToTable("Currency", "dbo");
            modelBuilder.Entity<Account>().ToTable("Account", "Banking");
            modelBuilder.Entity<AccountUser>().ToTable("AccountUser", "Banking");
            modelBuilder.Entity<AccountWithBalance>().ToTable("AccountWithBalance", "Banking");
            modelBuilder.Entity<Transfer>().ToTable("Transfer", "Banking");
        }

        private static void AddParam(DbCommand command, string name, DbType type, object value = null, ParameterDirection direction = ParameterDirection.Input, int? size = null)
        {
            var param = CreateParam(command, name, type, value, direction, size);
            command.Parameters.Add(param);
            return;
        }

        private static DbParameter CreateParam(
            DbCommand command,
            string name,
            DbType type,
            object value = null,
            ParameterDirection direction = ParameterDirection.Input,
            int? size = null)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;
            param.Direction = direction;

            if (size.HasValue)
            {
                param.Size = size.Value;
            }

            return param;
        }

        private DbCommand CreateCommand(string procedureName)
        {
            var command = this.Database.Connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParam(command, "@ReturnCode", DbType.Int32, direction: ParameterDirection.ReturnValue));

            return command;
        }

        private int ExecuteCommand(DbCommand command)
        {
            command.Connection.Open();
            command.ExecuteNonQuery();

            return (int)command.Parameters["@ReturnCode"].Value;
        }

        public int CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            var command = this.CreateCommand("CreateTransfer");

            AddParam(command, "@SourceAccountId", DbType.Int32, sourceAccountId);
            AddParam(command, "@DestAccountId", DbType.Int32, destAccountId);
            AddParam(command, "@DestAccountId", DbType.String, description);
            AddParam(command, "@Amount", DbType.Decimal, amount);
            AddParam(command, "@TransferId", DbType.Int32, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return (int)command.Parameters["@TransferId"].Value;
                default:
                    throw new Exception();
            }
        }

        public Guid CreateSession(int userId, string passwordHash)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(passwordHash), "passwordHash");
            Contract.Requires<ArgumentException>(userId > 0, "userId");

            var command = this.CreateCommand("Auth.CreateSession");
            AddParam(command, "@UserId", DbType.Int32, userId);
            AddParam(command, "@PasswordHash", DbType.String, passwordHash, size: 100);
            AddParam(command, "@SessionId", DbType.Guid, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return (Guid)command.Parameters["@SessionId"].Value;
                default:
                    throw new Exception();
            }
        }

        public int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            var command = this.CreateCommand("Auth.VerifySession");
            AddParam(command, "@UserId", DbType.Int32, direction: ParameterDirection.Output);
            AddParam(command, "@SessionId", DbType.Guid, sessionId);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return (int)command.Parameters["@UserId"].Value;
                case 100:
                    throw new SessionDoesNotExistException();
                default:
                    throw new Exception();
            }
        }
    }
}
