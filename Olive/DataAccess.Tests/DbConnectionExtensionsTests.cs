// -----------------------------------------------------------------------
// <copyright file="DbConnectionExtensionsTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class DbConnectionExtensionsTests
    {
        /// <summary>
        /// Tests the AddParam method.
        /// This test requires a lot of mocking, especially because of the final Contract.Ensures which
        /// makes sure that the parameter was added to the parameters list.
        /// </summary>
        [Test]
        public void AddParamTest()
        {
            var mockParam = new Mock<IDbDataParameter>().SetupAllProperties();

            var mockParams = new Mock<IDataParameterCollection>();
            mockParams.Setup(mp => mp.Add(It.Is<IDbDataParameter>(p => true)));
            mockParams.Setup(mp => mp[It.Is<string>(pn => pn == "ParamName")]).Returns(mockParam.Object);

            var mockCommand = new Mock<IDbCommand>();
            mockCommand.Setup(c => c.CreateParameter()).Returns(mockParam.Object);
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);

            IDbConnectionExtensions.AddParam(mockCommand.Object, "ParamName", DbType.Int32, value: 100, direction: ParameterDirection.Output, size: 4);
        }

        [Test]
        public void CreateCommandTest()
        {
            var mockParam = new Mock<IDbDataParameter>().SetupAllProperties();

            var mockParams = new Mock<IDataParameterCollection>();

            // Should always add @ReturnCode parameter.
            mockParams.Setup(mp => mp.Add(It.Is<IDbDataParameter>(p => p.ParameterName == "@ReturnCode")));
            mockParams.Setup(mp => mp[It.Is<string>(pn => pn == "@ReturnCode")]).Returns(mockParam.Object);

            var mockCommand = new Mock<IDbCommand>().SetupAllProperties();
            mockCommand.Setup(c => c.CreateParameter()).Returns(mockParam.Object);
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            IDbConnectionExtensions.CreateCommand(mockConnection.Object, "Schema.StoredProcedureName");

            // Verify that the @ReturnCode parameter is added.
            mockParams.Verify(mp => mp.Add(It.Is<IDbDataParameter>(p => p.ParameterName == "@ReturnCode")), Times.Once());
        }

        /// <summary>
        /// Tests the ExecuteCommand method.
        /// The ExecuteCommand method is expected to return the return code from the SQL Server.
        /// </summary>
        [Test]
        public void ExecuteCommandTest()
        {
            var mockParam = new Mock<IDbDataParameter>().SetupAllProperties();
            mockParam.Setup(p => p.Value).Returns(500);

            var mockParams = new Mock<IDataParameterCollection>();

            // Should always add @ReturnCode parameter.
            mockParams.Setup(mp => mp.Add(It.Is<IDbDataParameter>(p => p.ParameterName == "@ReturnCode")));
            mockParams.Setup(mp => mp[It.Is<string>(pn => pn == "@ReturnCode")]).Returns(mockParam.Object);

            var mockCommand = new Mock<IDbCommand>().SetupAllProperties();
            mockCommand.Setup(c => c.CreateParameter()).Returns(mockParam.Object);
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);

            var mockConnection = new Mock<IDbConnection>();
            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);

            var returnCode = IDbConnectionExtensions.ExecuteCommand(mockCommand.Object);

            Assert.AreEqual(500, returnCode);
        }

        [Test]
        public void GetReturnCodeTest()
        {
            var mockParam = new Mock<IDbDataParameter>().SetupAllProperties();
            mockParam.Setup(p => p.Value).Returns(1001);

            var mockParams = new Mock<IDataParameterCollection>();

            // Should always add @ReturnCode parameter.
            mockParams.Setup(mp => mp.Add(It.Is<IDbDataParameter>(p => p.ParameterName == "@ReturnCode")));
            mockParams.Setup(mp => mp[It.Is<string>(pn => pn == "@ReturnCode")]).Returns(mockParam.Object);

            var mockCommand = new Mock<IDbCommand>().SetupAllProperties();
            mockCommand.Setup(c => c.CreateParameter()).Returns(mockParam.Object);
            mockCommand.SetupGet(c => c.Parameters).Returns(mockParams.Object);

            Assert.AreEqual(1001, IDbConnectionExtensions.GetReturnCode(mockCommand.Object));
        }
    }
}
