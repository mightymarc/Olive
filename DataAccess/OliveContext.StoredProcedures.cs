// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveContext.StoredProcedures.cs" company="Olive">
//   Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    
//    1. Definitions
//    
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    
//    A "contribution" is the original software, or any additions or changes to the software.
//    
//    A "contributor" is any person that distributes its contribution under this license.
//    
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    
//    2. Grant of Rights
//    
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    
//    3. Conditions and Limitations
//    
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// </copyright>
// <summary>
//   The entity framework database context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// The entity framework database context.
    /// </summary>
    public partial class OliveContext
    {
        /// <summary>
        /// Creates a current account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="currencyId">The currency of the account.</param>
        /// <param name="displayName">The display name of the account (USD, BTC, PPUSD, ...)</param>
        /// <returns>
        /// The account id of the new account
        /// </returns>
        public int CreateCurrentAccount(int userId, string currencyId, string displayName)
        {
            var command = this.CommandConnection.CreateCommand("Banking.CreateCurrentAccount");
            command.AddParam("@UserId", DbType.Int32, userId);
            command.AddParam("@CurrencyId", DbType.AnsiString, currencyId);
            command.AddParam("@DisplayName", DbType.String, (object)displayName ?? DBNull.Value);
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
        /// Creates the session.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="passwordHash">
        /// The password Hash.
        /// </param>
        /// <returns>
        /// The session identifier.
        /// </returns>
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
        /// Creates the transfer.
        /// </summary>
        /// <param name="sourceAccountId">
        /// The source account id.
        /// </param>
        /// <param name="destAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The identifier for the transfer that was created.
        /// </returns>
        public long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            var command = this.CommandConnection.CreateCommand("Banking.CreateTransfer");
            command.AddParam("@SourceAccountId", DbType.Int32, sourceAccountId);
            command.AddParam("@DestAccountId", DbType.Int32, destAccountId);
            command.AddParam("@Amount", DbType.Decimal, amount);
            command.AddParam("@Description", DbType.String, (object)description ?? DBNull.Value);
            command.AddParam("@TransferId", DbType.Int64, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var transactionId = (long)command.GetParameter("@TransferId").Value;
                    Contract.Assume(transactionId > 0);
                    return transactionId;
                case 51001:
                    throw new AuthorizationException(
                        "The user does not have withdraw access to the specified source account.");
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <exception cref="UnknownReturnCodeException">
        /// </exception>
        public void EditCurrentAccount(int accountId, string displayName)
        {
            var command = this.CommandConnection.CreateCommand("Banking.EditCurrentAccount");
            command.AddParam("@AccountId", DbType.Int32, accountId);
            command.AddParam("@DisplayName", DbType.String, (object)displayName ?? DBNull.Value);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">
        /// The command to execute.
        /// </param>
        /// <returns>
        /// The result code returned by the RETURN command of Transact SQL.
        /// </returns>
        public virtual int ExecuteCommand(IDbCommand command)
        {
            return command.ExecuteCommand();
        }

        /// <summary>
        /// Verifies that specified session exists and is not expired.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
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

        public string GetLastProcessedTransactionId()
        {
            var command = this.CommandConnection.CreateCommand("Bitcoin.[GetLastProcessedTransactionId]");
            command.AddParam("@TransactionId", DbType.AnsiString, size: 64, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var transactionId = command.GetParameter("@TransactionId").Value;
                    return transactionId == DBNull.Value ? null : (string)transactionId;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        public virtual bool BitcoinTransactionIsProcessed(string transactionId)
        {
            return this.BitcoinTransactions.Any(x => x.TransactionId == transactionId);
        }

        public int CreateAccountHold(int accountId, decimal amount, string holdReason, DateTime? expiresAt)
        {
            var command = this.CommandConnection.CreateCommand("Banking.CreateAccountHold");
            command.AddParam("@AccountId", DbType.Int32, accountId);
            command.AddParam("@Amount", DbType.Decimal, amount);
            command.AddParam("@Reason", DbType.String, holdReason ?? (object)DBNull.Value, size: 150);
            command.AddParam("@ExpiresAt", DbType.DateTime, expiresAt != null ? (object)expiresAt : DBNull.Value);
            command.AddParam("@AccountHoldId", DbType.Int32, direction: ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    var accountHoldId = command.GetParameter("@AccountHoldId").Value;
                    return (int)accountHoldId;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        public int GetSpecialAccountId(string name)
        {
            var command = this.CommandConnection.CreateCommand();
            command.CommandText = "SELECT Banking.GetSpecialAccountId(@Name)";
            command.AddParam("@Name", DbType.AnsiString, name, size: 150);

            command.Connection.Open();

            try
            {
                var result = command.ExecuteScalar();

                if (result == DBNull.Value)
                {
                    throw new Exception("The special account " + name + " was not found.");
                }

                return (int)result;
            }
            finally
            {
                command.Connection.Close();   
            }
        }

        public void CreateTransaction(string transactionId, int accountId, int accountHoldId, decimal amount)
        {
            var command = this.CommandConnection.CreateCommand("Bitcoin.CreateTransaction");
            command.AddParam("@TransactionId", DbType.AnsiString, transactionId, size: 64);
            command.AddParam("@AccountId", DbType.Int32, accountId);
            command.AddParam("@AccountHoldId", DbType.Int32, accountHoldId);
            command.AddParam("@Amount", DbType.Decimal, amount);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        public void SetAccountReceiveAddress(int accountId, string receiveAddress)
        {
            var command = this.CommandConnection.CreateCommand("Bitcoin.SetAccountReceiveAddress");
            command.AddParam("@AccountId", DbType.Int32, accountId);
            command.AddParam("@ReceiveAddress", DbType.AnsiString, receiveAddress, size: 34);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        public string GetAccountReceiveAddress(int accountId)
        {
            var command = this.CommandConnection.CreateCommand("Bitcoin.GetAccountReceiveAddress");
            command.AddParam("@AccountId", DbType.Int32, accountId);
            command.AddParam("@ReceiveAddress", DbType.AnsiString, 34, ParameterDirection.Output);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return (string)command.Parameters["@ReceiveAddress"];
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }

        public void ReleaseAccountHold(int accountHoldId)
        {
            var command = this.CommandConnection.CreateCommand("Banking.ReleaseAccountHold");
            command.AddParam("@AccountHoldId", DbType.Int32, accountHoldId);

            switch (this.ExecuteCommand(command))
            {
                case 0:
                    return;
                default:
                    throw new UnknownReturnCodeException(command.GetReturnCode());
            }
        }
    }
}
