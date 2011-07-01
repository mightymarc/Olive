// -----------------------------------------------------------------------
// <copyright file="AdoHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class AdoExtensions
    {
        public static SqlCommand CreateSp(
            this SqlConnection connection,
            string procedureName,
            params SqlParameter[] parameters)
        {
            var command = new SqlCommand
                { Connection = connection, CommandText = procedureName, CommandType = CommandType.StoredProcedure };

            command.Parameters.AddRange(parameters);
            command.Parameters.Add(
                new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue,
                        ParameterName = "@ReturnCode",
                        Size = 4,
                        SqlDbType = SqlDbType.Int
                    });

            return command;
        }

        public static int ExecuteSp(this SqlCommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Assume(command.Parameters["@ReturnCode"] != null, "The @ReturnCode is parameter missing from the SqlCommand.");

            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
            }

            Contract.Assume(command.Connection.State == ConnectionState.Open);

            command.ExecuteNonQuery();

            return (int)command.Parameters["@ReturnCode"].Value;
        }

        public static Exception CreateUnknownErrorCodeException(this SqlCommand command)
        {
            return new Exception("Unknown return code.");
        }
    }
}
