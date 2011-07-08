// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdoHelper.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics.Contracts;

    public static class IDbConnectionExtensions
    {
        /// <summary>
        /// Creates a parameter with the specified properties and adds it to the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="size">The size.</param>
        public static void AddParam(
            this IDbCommand command,
            string name,
            DbType type,
            object value = null,
            ParameterDirection direction = ParameterDirection.Input,
            int? size = null)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Requires<ArgumentNullException>(command.Parameters != null, "command.Parameters");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name), "name");
            Contract.Ensures(command.Parameters[name] != null);

            var param = CreateParam(command, name, type, value, direction, size);
            command.Parameters.Add(param);
            return;
        }

        /// <summary>
        /// Creates a parameter for the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <param name="size">The size of the type in bytes.</param>
        /// <returns>The parameter.</returns>
        private static IDbDataParameter CreateParam(
            this IDbCommand command,
            string name,
            DbType type,
            object value = null,
            ParameterDirection direction = ParameterDirection.Input,
            int? size = null)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name), "name");
            Contract.Ensures(Contract.Result<IDbDataParameter>() != null);

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

        /// <summary>
        /// Creates a command.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="procedureName">The name of the stored procedure.</param>
        /// <returns>
        /// The command that was created.
        /// </returns>
        public static IDbCommand CreateCommand(this IDbConnection connection, string procedureName)
        {
            Contract.Requires<ArgumentNullException>(connection != null, "connection");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(procedureName), "procedureName");

            var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;

            command.Parameters.Add(
                CreateParam(command, "@ReturnCode", DbType.Int32, direction: ParameterDirection.ReturnValue));

            return command;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The return code from the procedure (RETURN statement)</returns>
        public static int ExecuteCommand(this IDbCommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Requires<ArgumentNullException>(command.Connection != null, "command.Connection");

            command.Connection.Open();
            command.ExecuteNonQuery();

            Contract.Assume(command.Parameters["@ReturnCode"] != null);

            return command.GetReturnCode();
        }

        public static IDbDataParameter GetParameter(this IDbCommand command, string name)
        {
            return (IDbDataParameter)command.Parameters[name];
        }

        /// <summary>
        /// Gets the return code from the specified command's @ReturnCode parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The return code.</returns>
        public static int GetReturnCode(this IDbCommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Assume(command.Parameters["@ReturnCode"] != null);
            Contract.Assume(command.GetParameter("@ReturnCode").Value is int, "The procedure did not return a return code.");

            return (int)command.GetParameter("@ReturnCode").Value;
        }
    }
}