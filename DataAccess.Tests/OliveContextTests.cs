// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveContextTests.cs" company="Olive">
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
//   Defines the OliveContextTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Data;
    using System.Globalization;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// The olive context tests.
    /// </summary>
    public class OliveContextTests : TestBase
    {
        /// <summary>
        /// The create current account success test.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="currencyId">
        /// The currency id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCase(1, "USD", null)]
        [TestCase(5, "BTC", null)]
        [TestCase(1000, "MBUSD", "")]
        public void CreateCurrentAccountSuccessTest(int userId, string currencyId, string displayName)
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@AccountId").Value = 100;
                        mockCommand.Object.GetParameter("@ReturnCode").Value = 0;
                        return 0;
                    });

            // Act and assert
            Assert.AreEqual(100, mockContext.Object.CreateCurrentAccount(userId, currencyId, displayName));
        }

        /// <summary>
        /// The create current account throws exception on unknown return code.
        /// </summary>
        [Test]
        public void CreateCurrentAccountThrowsExceptionOnUnknownReturnCode()
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@ReturnCode").Value = 12345;
                        return 12345;
                    });

            // Act and assert
            Assert.Throws<UnknownReturnCodeException>(() => mockContext.Object.CreateCurrentAccount(1, "BTC", null));
        }

        /// <summary>
        /// Creates a current account with bad arguments, expecting that an exception is thrown.
        ///   Note that empty sting display names are not allowed at this point, null should be used instead.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="currencyId">
        /// The currency id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCase(0, "USD", null)]
        [TestCase(-5, "USD", null)]
        [TestCase(1, "", null)]
        [TestCase(1, null, null)]
        [TestCase(1, null, "")]
        public void CreateCurrentAccountWithBadArgumentsThrowsException(
            int userId, string currencyId, string displayName)
        {
            var mockContext = new Mock<OliveContext>();

            try
            {
                mockContext.Object.CreateCurrentAccount(userId, currencyId, displayName);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "Expected exception with UserId={0}; CurrencyId={1}; DisplayName={2}", 
                    userId, 
                    currencyId, 
                    displayName));
        }

        /// <summary>
        /// The create session throws exception on unknown return code.
        /// </summary>
        [Test]
        public void CreateSessionThrowsExceptionOnUnknownReturnCode()
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@ReturnCode").Value = 123;
                        return 123;
                    });

            // Act and assert
            Assert.Throws<UnknownReturnCodeException>(() => mockContext.Object.CreateSession("email@pass.com", "hash"));
        }

        /// <summary>
        /// The create transfer returns transfer id.
        /// </summary>
        [Test]
        public void CreateTransferReturnsTransferId()
        {
            var transferId = 100L;

            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@TransferId").Value = transferId;

                        return 0;
                    });

            // Act and assert
            Assert.AreEqual(transferId, mockContext.Object.CreateTransfer(1, 2, "test", 15));
        }

        /// <summary>
        /// The create transfer throws exception on unknown return code.
        /// </summary>
        [Test]
        public void CreateTransferThrowsExceptionOnUnknownReturnCode()
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@ReturnCode").Value = 12345;
                        return 123;
                    });

            // Act and assert
            Assert.Throws<UnknownReturnCodeException>(() => mockContext.Object.CreateTransfer(1, 2, "test", 100));
        }

        /// <summary>
        /// The creates session returns session id.
        /// </summary>
        [Test]
        public void CreatesSessionReturnsSessionId()
        {
            var sessionId = Guid.NewGuid();

            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@SessionId").Value = sessionId;

                        return 0;
                    });

            // Act and assert
            Assert.AreEqual(sessionId, mockContext.Object.CreateSession("email@pass.com", "hash"));
        }

        /// <summary>
        /// The edit account with bad arguments throws exception.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCase(100, "Desc")]
        [TestCase(2, null)]
        public void EditAccountWithBadArgumentsThrowsException(int accountId, string displayName)
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () => (int)(mockCommand.Object.GetParameter("@ReturnCode").Value = 51009));

            // Act and assert
            Assert.Throws<UnknownReturnCodeException>(
                () => mockContext.Object.EditCurrentAccount(accountId, displayName));
        }

        /// <summary>
        /// The edit account with does not throw exception.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCase(100, "Desc")]
        [TestCase(2, null)]
        public void EditAccountWithDoesNotThrowException(int accountId, string displayName)
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () => (int)(mockCommand.Object.GetParameter("@ReturnCode").Value = 0));

            // Act
            mockContext.Object.EditCurrentAccount(accountId, displayName);
        }

        /// <summary>
        /// The verify session returns user id.
        /// </summary>
        [Test]
        public void VerifySessionReturnsUserId()
        {
            var userId = 100;

            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@UserId").Value = userId;

                        return 0;
                    });

            // Act and assert
            Assert.AreEqual(userId, mockContext.Object.VerifySession(Guid.NewGuid()));
        }

        /// <summary>
        /// The verify session throws exception on unknown return code.
        /// </summary>
        [Test]
        public void VerifySessionThrowsExceptionOnUnknownReturnCode()
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () =>
                    {
                        mockCommand.Object.GetParameter("@ReturnCode").Value = 123;
                        return 123;
                    });

            // Act and assert
            Assert.Throws<UnknownReturnCodeException>(() => mockContext.Object.CreateSession("email@pass.com", "hash"));
        }

        /// <summary>
        /// The verify session with non existing session.
        /// </summary>
        [Test]
        public void VerifySessionWithNonExistingSession()
        {
            // Arrange
            var mockCommand = UnitTestHelper.CreateMockDbCommand();
            var mockContext = new Mock<OliveContext>();
            mockContext.Setup(c => c.CommandConnection).Returns(mockCommand.Object.Connection);
            mockContext.Setup(c => c.ExecuteCommand(It.IsAny<IDbCommand>())).Returns(
                () => (int)(mockCommand.Object.GetParameter("@ReturnCode").Value = 51009));

            // Act and assert
            Assert.Throws<UnknownReturnCodeException>(() => mockContext.Object.VerifySession(Guid.NewGuid()));
        }
    }
}