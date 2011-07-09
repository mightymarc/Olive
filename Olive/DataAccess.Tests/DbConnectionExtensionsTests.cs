// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbConnectionExtensionsTests.cs" company="Olive">
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
//   Defines the DbConnectionExtensionsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System.Data;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// The db connection extensions tests.
    /// </summary>
    [TestFixture]
    public class DbConnectionExtensionsTests
    {
        #region Public Methods

        /// <summary>
        /// Tests the AddParam method.
        ///   This test requires a lot of mocking, especially because of the final Contract.Ensures which
        ///   makes sure that the parameter was added to the parameters list.
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

            mockCommand.Object.AddParam(
                "ParamName", DbType.Int32, value: 100, direction: ParameterDirection.Output, size: 4);
        }

        /// <summary>
        /// The create command test.
        /// </summary>
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

            mockConnection.Object.CreateCommand("Schema.StoredProcedureName");

            // Verify that the @ReturnCode parameter is added.
            mockParams.Verify(
                mp => mp.Add(It.Is<IDbDataParameter>(p => p.ParameterName == "@ReturnCode")), Times.Once());
        }

        /// <summary>
        /// Tests the ExecuteCommand method.
        ///   The ExecuteCommand method is expected to return the return code from the SQL Server.
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

            var returnCode = mockCommand.Object.ExecuteCommand();

            Assert.AreEqual(500, returnCode);
        }

        /// <summary>
        /// The get return code test.
        /// </summary>
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

            Assert.AreEqual(1001, mockCommand.Object.GetReturnCode());
        }

        #endregion
    }
}