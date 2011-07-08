// -----------------------------------------------------------------------
// <copyright file="OliveContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///   The entity framework database context.
    /// </summary>
    public partial class OliveContext
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The result code returned by the RETURN command of Transact SQL.</returns>
        public virtual int ExecuteCommand(IDbCommand command)
        {
            return command.ExecuteCommand();
        }

        /// <summary>
        /// Creates the session.
        /// </summary>
        /// <returns>The session identifier.</returns>
        public Guid CreateSession(string email, string passwordHash)
        {
            var command = this.CommandConnection.CreateCommand("Auth.CreateSession");
            command.AddParam("@Email", DbType.String, email);
            command.AddParam("@PasswordHash", DbType.String, passwordHash, size: 100);
            command.AddParam("@SessionId", DbType.Guid, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var sessionId = (Guid)command.GetParameter("@SessionId").Value;
                    Contract.Assume(sessionId != Guid.Empty);
                    return sessionId;
                case 51009:
                    throw new AuthenticationException();
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        /// <summary>
        /// Creates a current account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="currencyId">The currency of the account.</param>
        /// <param name="displayName">The display name of the account (USD, BTC, PPUSD, ...)</param>
        /// <returns>The account id of the new account</returns>
        public int CreateCurrentAccount(int userId, string currencyId, string displayName)
        {
            var command = this.CommandConnection.CreateCommand("Banking.CreateCurrentAccount");
            command.AddParam("@UserId", DbType.Int32, userId);
            command.AddParam("@CurrencyId", DbType.AnsiString, currencyId);
            command.AddParam("@DisplayName", DbType.String, displayName);
            command.AddParam("@AccountId", DbType.Int32, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var accountId = (int)command.GetParameter("@AccountId").Value;
                    Contract.Assume(accountId > 0);
                    return accountId;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
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
            var command = this.CommandConnection.CreateCommand("Banking.CreateTransfer");
            command.AddParam("@SourceAccountId", DbType.Int32, sourceAccountId);
            command.AddParam("@DestAccountId", DbType.Int32, destAccountId);
            command.AddParam("@Amount", DbType.Decimal, amount);
            command.AddParam("@Description", DbType.String, description);
            command.AddParam("@TransferId", DbType.Int64, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var transactionId = (long)command.GetParameter("@TransferId").Value;
                    Contract.Assume(transactionId > 0);
                    return transactionId;
                case 51001:
                    throw new AuthorizationException("The user does not have withdraw access to the specified source account.");
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
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
            var command = this.CommandConnection.CreateCommand("Auth.VerifySession");
            command.AddParam("@UserId", DbType.Int32, direction: ParameterDirection.Output);
            command.AddParam("@SessionId", DbType.Guid, sessionId);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var userId = (int)command.GetParameter("@UserId").Value;
                    Contract.Assume(userId > 0);
                    return userId;
                case 51011:
                    throw new SessionDoesNotExistException();
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }
    }
}
