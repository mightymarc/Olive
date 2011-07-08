// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestHelper.cs" company="">
//   
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
    ///   Summary description for Class1.
    /// </summary>
    public class UnitTestHelper
    {
        private UnitTestHelper()
        {
        }

        // end of method
        public static object RunInstanceMethod(Type t, string strMethod, object objInstance, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, objInstance, aobjParams, eFlags);
        }

        /// <summary>
        ///   Runs a method on a type, given its parameters. This is useful for
        ///   calling private methods.
        /// </summary>
        /// <param name = "t">The t.</param>
        /// <param name = "strMethod">The STR method.</param>
        /// <param name = "aobjParams">The aobj params.</param>
        /// <returns>
        ///   The return value of the called method.
        /// </returns>
        public static object RunStaticMethod(Type t, string strMethod, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, null, aobjParams, eFlags);
        }

        // end of method

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

        public static IDbCommand CreateMockDbCommand(IDbConnection connection, params Tuple<string, object>[] parameterValues)
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

        public static Mock<IDbCommand> CreateMockDbCommand()
        {
            var mockParams = new MockDataParameterCollection();

            var mockCommand = new Mock<IDbCommand>();
            mockCommand.SetupAllProperties();
            mockCommand.SetupGet(x => x.Parameters).Returns(mockParams);
            mockCommand.Setup(c => c.CreateParameter()).Returns(() => new Mock<IDbDataParameter>().SetupAllProperties().Object);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.SetupGet(c => c.Connection).Returns(mockConnection.Object);

            return mockCommand;
        }


        // end of method
    }

    // end of class
}

//end of namespace