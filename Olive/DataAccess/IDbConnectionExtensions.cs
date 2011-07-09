// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbConnectionExtensions.cs" company="Olive">
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
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The i db connection extensions.
    /// </summary>
    public static class IDbConnectionExtensions
    {
        #region Public Methods

        /// <summary>
        /// Creates a parameter with the specified properties and adds it to the specified command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
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
        /// Creates a command.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="procedureName">
        /// The name of the stored procedure.
        /// </param>
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
        /// <param name="command">
        /// The command to execute.
        /// </param>
        /// <returns>
        /// The return code from the procedure (RETURN statement)
        /// </returns>
        public static int ExecuteCommand(this IDbCommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Requires<ArgumentNullException>(command.Connection != null, "command.Connection");

            command.Connection.Open();
            command.ExecuteNonQuery();

            Contract.Assume(command.Parameters["@ReturnCode"] != null);

            return command.GetReturnCode();
        }

        /// <summary>
        /// The get parameter.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// </returns>
        public static IDbDataParameter GetParameter(this IDbCommand command, string name)
        {
            return (IDbDataParameter)command.Parameters[name];
        }

        /// <summary>
        /// Gets the return code from the specified command's @ReturnCode parameter.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The return code.
        /// </returns>
        public static int GetReturnCode(this IDbCommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null, "command");
            Contract.Assume(command.Parameters["@ReturnCode"] != null);
            Contract.Assume(
                command.GetParameter("@ReturnCode").Value is int, "The procedure did not return a return code.");

            return (int)command.GetParameter("@ReturnCode").Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a parameter for the specified command.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="direction">
        /// The direction of the parameter.
        /// </param>
        /// <param name="size">
        /// The size of the type in bytes.
        /// </param>
        /// <returns>
        /// The parameter.
        /// </returns>
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

        #endregion
    }
}