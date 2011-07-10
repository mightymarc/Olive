﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestHelper.cs" company="Olive">
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
//   Summary description for Class1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive
{
    using System;
    using System.Data;
    using System.Reflection;

    using Moq;

    /// <summary>
    /// Static methods and extension methods to help unit testing.
    /// </summary>
    public static class UnitTestHelper
    {
        /// <summary>
        /// The random.
        /// </summary>
        public static Random Random = new Random();

        /// <summary>
        /// The create mock db command.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="parameterValues">
        /// The parameter values.
        /// </param>
        /// <returns>
        /// </returns>
        public static IDbCommand CreateMockDbCommand(
            IDbConnection connection, params Tuple<string, object>[] parameterValues)
        {
            var mockCommand = new Mock<IDbCommand>();
            mockCommand.SetupAllProperties();
            mockCommand.Object.Connection = connection;

            var mockParams = new Mock<IDataParameterCollection>();
            var mockReturnParam = new Mock<IDbDataParameter>();
            mockReturnParam.SetupGet(p => p.Value).Returns(0);

            foreach (var parameterValue in parameterValues)
            {
                var param = new Mock<IDbDataParameter>();
                param.SetupGet(p => p.Value).Returns(parameterValue.Item2);

                mockParams.SetupGet(c => c[parameterValue.Item1]).Returns(param.Object);
            }

            mockCommand.Setup(c => c.Parameters).Returns(mockParams.Object);

            return mockCommand.Object;
        }

        /// <summary>
        /// The create mock db command.
        /// </summary>
        /// <returns>
        /// </returns>
        public static Mock<IDbCommand> CreateMockDbCommand()
        {
            var mockParams = new MockDataParameterCollection();

            var mockCommand = new Mock<IDbCommand>();
            mockCommand.SetupAllProperties();
            mockCommand.SetupGet(x => x.Parameters).Returns(mockParams);
            mockCommand.Setup(c => c.CreateParameter()).Returns(
                () => new Mock<IDbDataParameter>().SetupAllProperties().Object);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.SetupGet(c => c.Connection).Returns(mockConnection.Object);

            return mockCommand;
        }

        // end of method
        /// <summary>
        /// The run instance method.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="strMethod">
        /// The str method.
        /// </param>
        /// <param name="objInstance">
        /// The obj instance.
        /// </param>
        /// <param name="aobjParams">
        /// The aobj params.
        /// </param>
        /// <returns>
        /// The run instance method.
        /// </returns>
        public static object RunInstanceMethod(Type t, string strMethod, object objInstance, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, objInstance, aobjParams, eFlags);
        }

        /// <summary>
        /// Runs a method on a type, given its parameters. This is useful for
        ///   calling private methods.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="strMethod">
        /// The STR method.
        /// </param>
        /// <param name="aobjParams">
        /// The aobj params.
        /// </param>
        /// <returns>
        /// The return value of the called method.
        /// </returns>
        public static object RunStaticMethod(Type t, string strMethod, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, null, aobjParams, eFlags);
        }

        // end of method

        /// <summary>
        /// The run method.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="strMethod">
        /// The str method.
        /// </param>
        /// <param name="objInstance">
        /// The obj instance.
        /// </param>
        /// <param name="aobjParams">
        /// The aobj params.
        /// </param>
        /// <param name="eFlags">
        /// The e flags.
        /// </param>
        /// <returns>
        /// The run method.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static object RunMethod(
            Type t, string strMethod, object objInstance, object[] aobjParams, BindingFlags eFlags)
        {
            MethodInfo m;
            try
            {
                m = t.GetMethod(strMethod, eFlags);
                if (m == null)
                {
                    throw new ArgumentException("There is no method '" + strMethod + "' for type '" + t + "'.");
                }

                object objRet = m.Invoke(objInstance, aobjParams);
                return objRet;
            }
            catch
            {
                throw;
            }
        }

        // end of method
    }
    // end of class
}

//end of namespace