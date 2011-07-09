// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveContextTests.cs" company="Olive">
//   
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

    public class OliveContextTests : TestBase
    {
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
        ///   Creates a current account with bad arguments, expecting that an exception is thrown.
        ///   Note that empty sting display names are not allowed at this point, null should be used instead.
        /// </summary>
        /// <param name = "userId">The user id.</param>
        /// <param name = "currencyId">The currency id.</param>
        /// <param name = "displayName">The display name.</param>
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
            Assert.Throws<UnknownReturnCodeException>(() => mockContext.Object.EditCurrentAccount(accountId, displayName));
        }

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