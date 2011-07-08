// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebServiceTests.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the WebServiceTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.DataAccess;
    using Olive.DataAccess.Tests;

    [TestFixture]
    public sealed class WebServiceTests
    {
        private static object[] createTransferWithBadArgumentsThrowsExceptionCases = {
                                                                                         new object[] { 0, 1, 100m, "cc" }
                                                                                         , new object[] { 1, 1, 100m, "" }
                                                                                         ,
                                                                                         new object[]
                                                                                             { -1, 1, 100m, "cc" },
                                                                                         new object[] { 1, 1, -100m, "" },
                                                                                         new object[]
                                                                                             { 1, -1, 100m, "cc" },
                                                                                         new object[] { 1, 1, 0m, "cc" },
                                                                                         new object[] { 1, 1, 1m, "cc" },
                                                                                         new object[]
                                                                                             { 1, 0, 100m, "des" }
                                                                                     };

        private static object[] editAccountDoesNotThrowExceptionCases = {
                                                                            new object[] { Guid.NewGuid(), 123, "Name" }, 
                                                                            new object[]
                                                                                {
                                                                                   Guid.NewGuid(), 1, string.Empty 
                                                                                }, 
                                                                            new object[]
                                                                                {
                                                                                   Guid.NewGuid(), 333433434, null 
                                                                                }
                                                                        };

        private static object[] editAccountWithBadArgumentsThrowsExceptionCases = {
                                                                                      new object[]
                                                                                          {
                                                                                              Guid.Empty, 1, string.Empty
                                                                                          }, 
                                                                                      new object[]
                                                                                          {
                                                                                              Guid.NewGuid(), 0, 
                                                                                              string.Empty
                                                                                          }, 
                                                                                      new object[]
                                                                                          {
                                                                                             Guid.NewGuid(), -1, string.Empty 
                                                                                          }, 
                                                                                      new object[]
                                                                                          {
                                                                                             Guid.NewGuid(), -1, null 
                                                                                          }
                                                                                  };

        private IUnityContainer container;

        [Test]
        public void CreateSessionWitInvalidEmailThrowsException()
        {
            var service = new WebService();

            Assert.Throws<ArgumentException>(() => service.CreateSession("@bademail.com", "password"));
        }

        /*[Test]
        public void CreateSessionWithCorrectCredentialsReturnsSessionId()
        {
            var email = "email@pass.com";
            var passwordHash = "passwordHash";
            var sessionId = Guid.NewGuid();

            // Mock a IOliveContext which contains pre-defined users.
            var context = new Mock<IOliveContext>().Object;
            Mock.Get(context).SetupProperty(
                c => c.Users, 
                new MockDbSet<User> { new User { Email = email, PasswordHash = "hash", PasswordSalt = "salt" } });

            // Mock the ICrypto service to always create the same salt and hashes.
            var mockCrypto = new Mock<ICrypto>();
            mockCrypto.Setup(c => c.CreateSalt()).Returns("salt");
            mockCrypto.Setup(c => c.GenerateHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");

            this.container.RegisterInstance(mockCrypto.Object);
            this.container.RegisterInstance(context);

            var service = new WebService { Container = this.container };

            Mock.Get(context).Setup(c => c.CreateSession(email, It.IsAny<string>())).Returns(sessionId);

            Assert.AreEqual(sessionId, service.CreateSession(email, passwordHash));
            Mock.Get(context).Verify(c => c.CreateSession(email, It.IsAny<string>()), Times.Once());
        }*/

        [Test]
        public void CreateSessionWithNullEmailThrowsException()
        {
            var service = new WebService();

            Assert.Throws<ArgumentException>(() => service.CreateSession(null, "password"));
        }

        [Test]
        public void CreateSessionWithNullPasswordThrowsException()
        {
            var service = new WebService();

            Assert.Throws<ArgumentException>(() => service.CreateSession("valid@email.com", null));
        }

        [Test]
        public void CreateSessionWithUnknownEmailThrowsAuthenticationFault()
        {
            var email = "email@pass.com";
            var passwordHash = "passwordHash";

            var context = new MockOliveContext();
            this.container.RegisterInstance<IOliveContext>(context);
            var service = new WebService { Container = this.container };

            Assert.Throws<FaultException>(() => service.CreateSession(email, passwordHash));
        }

        [Test]
        public void CreateUserDoesNotThrowException()
        {
            // Arrange
            var email = "valid@email.com";
            var password = "passwordHash";

            // Mock the IOliveContext
            var contextMock = new Mock<IOliveContext>();
            contextMock.SetupProperty(c => c.Users, new MockDbSet<User>());
            this.container.RegisterInstance(contextMock.Object);

            // Mock the ICrypto service.
            var mockCrypto = new Mock<ICrypto>();
            mockCrypto.Setup(c => c.CreateSalt()).Returns("salt");
            mockCrypto.Setup(c => c.GenerateHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            this.container.RegisterInstance(mockCrypto.Object);

            var service = new WebService { Container = this.container };

            // Act
            service.CreateUser(email, password);

            // Assert
            Assert.IsNotNull(contextMock.Object.Users.SingleOrDefault(u => u.Email == email));
        }

        [Test]
        public void CreateUserWithAlreadyRegisteredEmailThrowsException()
        {
            // Arrange
            var email = "user@email.com";
            var password = "passwordHash";
            var context = new MockOliveContext();
            this.container.RegisterInstance<IOliveContext>(context);

            context.Users.Add(
                new User { Email = "USER@EMAIL.com", PasswordHash = "hash", PasswordSalt = "salt", UserId = 100 });

            // Act
            var service = new WebService { Container = this.container };

            // Assert
            Assert.Throws<FaultException>(() => service.CreateUser(email, password));
        }

        [Test]
        [TestCase("@email.com")]
        [TestCase(" @email.com")]
        [TestCase("@email.com ")]
        [TestCase("@email\t.com")]
        [TestCase("\r@email.com")]
        [TestCase("test\n@emailcom")]
        public void CreateUserWithBadEmailFormatThrowsException(string email)
        {
            // Arrange
            var password = "passwordHash";
            var context = new MockOliveContext();
            this.container.RegisterInstance<IOliveContext>(context);

            // Act
            var service = new WebService { Container = this.container };

            // Assert
            Assert.Throws<ArgumentException>(
                () => service.CreateUser(email, password),
                string.Format(
                    CultureInfo.CurrentCulture, "E-mail '{0}' should not have been allowed to register.", email));
        }

        [Test]
        public void CreateUserWithNullEmailThrowsException()
        {
            // Arrange
            var service = new WebService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser(null, "password"));
        }

        [Test]
        public void CreateUserWithNullPasswordThrowsException()
        {
            // Arrange
            var service = new WebService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser("a@b.com", null));
        }

        [Test]
        public void CreateCurrentAccountWithoutCredentialsThrowsException()
        {
            // Arrange
            var service = new WebService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateCurrentAccount(Guid.Empty, "a@b.com", null));
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "name")]
        [TestCase("", "name")]
        public void CreateCurrentAccountWithBadArgumentsThrowsException(string currencyId, string displayName)
        {
            var service = new WebService() { Container = this.container };
            var sessionId = Guid.NewGuid();

            try
            {
                service.CreateCurrentAccount(sessionId, currencyId, displayName);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail();
        }

        [Test]
        [TestCase("USD", null)]
        [TestCase("USD", "")]
        [TestCase("BTC", "Name of account to create")]
        public void CreateAccountWithGoodCredentialsDoesNotThrowException(string currencyId, string displayName)
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            var service = new WebService { Container = this.container };
            var sessionId = Guid.NewGuid();

            var expectedAccountId = 53;

            var userId = 512;

            context.Setup(c => c.VerifySession(sessionId)).Returns(userId);

            // Create account should convert empty strings to null because it's not appropriate in the database.
            context.Setup(c => c.CreateCurrentAccount(userId, currencyId, displayName == string.Empty ? null : displayName)).Returns(expectedAccountId);

            // Act
            var actualAccountId = service.CreateCurrentAccount(sessionId, currencyId, displayName);

            // Assert
            Assert.AreEqual(expectedAccountId, actualAccountId);
        }

        [Test]
        public void CreateUserWithTooShortPasswordThrowsException()
        {
            // Arrange
            var service = new WebService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser("a@b.c", "ab"));
        }

        [Test]
        [TestCaseSource("editAccountDoesNotThrowExceptionCases")]
        public void EditCurrentAccountDoesNotThrowException(Guid sessionId, int accountId, string displayName)
        {
            Assert.Inconclusive();
        }

        [Test]
        [TestCaseSource("editAccountWithBadArgumentsThrowsExceptionCases")]
        public void EditCurrentAccountWithBadArgumentsThrowsException(Guid sessionId, int accountId, string displayName)
        {
            Assert.Inconclusive();
        }

        [Test]
        public void EditCurrentAccountWithoutAuthenticationTest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void EditCurrentAccountWithoutHavingAccessTest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GetCurrenciesTest()
        {
            // Arrange
            var service = new WebService { Container = this.container };

            Assert.Inconclusive("Requires mock DbSet, postponed.");
        }

        [Test]
        public void CreateTransferWithoutAuthenticationThrowsException()
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            IWebService service = new WebService() { Container = this.container };

            var sesionId = Guid.NewGuid();
            var sourceAccountId = 1;
            var destAccountId = 2;
            var amount = 100.5m;
            var description = "Because";

            context.Setup(c => c.VerifySession(sesionId)).Throws(new SessionDoesNotExistException());

            Assert.Throws<FaultException>(() => service.CreateTransfer(sesionId, sourceAccountId, destAccountId, amount, description));

            // Assert
            context.Verify(c => c.VerifySession(sesionId), Times.Once());
        }

        [Test]
        [TestCaseSource("createTransferWithBadArgumentsThrowsExceptionCases")]
        public void CreateTransferWithBadArgumentsThrowsException(int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            context.Setup(c => c.VerifySession(It.IsAny<Guid>())).Returns(1);

            IWebService service = new WebService() { Container = this.container };

            try
            {
                service.CreateTransfer(Guid.NewGuid(), sourceAccountId, destAccountId, amount, description);
            }
            catch (ArgumentException)
            {
                return;
            }


            Assert.Fail("Unsupported case did not throw exception.");
        }

        [Test]
        public void CreateTransferWithoutAccessThrowsException()
        {
            // Arrange
            var userId = 1001;
            var sourceAccountId = 15;
            var destAccountId = 12;
            var description = "desc";
            var amount = 13.54m;

            var context = new Mock<IOliveContext>(MockBehavior.Strict);
            context.Setup(c => c.Dispose());
            context.Setup(c => c.VerifySession(It.IsAny<Guid>())).Returns(userId);
            this.container.RegisterInstance(context.Object);

            var service = new Mock<WebService>(MockBehavior.Strict);
            service.CallBase = true;
            service.Object.Container = this.container;
            service.Setup(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId)).Returns(false);

            // Act
            Assert.Throws<FaultException>(() => service.Object.CreateTransfer(Guid.NewGuid(), sourceAccountId, destAccountId, amount, description));

            // Assert
            service.Verify(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId), Times.Once());
        }

        [Test]
        public void CreateTransferSuccessDoesNotThrowException()
        {
            // Arrange
            var userId = 1001;
            var sourceAccountId = 15;
            var destAccountId = 12;
            var description = "desc";
            var amount = 13.54m;
            var transferId = 10002L;

            var context = new Mock<IOliveContext>(MockBehavior.Strict);
            context.Setup(c => c.Dispose());
            context.Setup(c => c.VerifySession(It.IsAny<Guid>())).Returns(userId);
            context.Setup(c => c.CreateTransfer(sourceAccountId, destAccountId, description, amount)).Returns(transferId);
            this.container.RegisterInstance(context.Object);

            var service = new Mock<WebService>(MockBehavior.Strict);
            service.CallBase = true;
            service.Object.Container = this.container;
            service.Setup(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId)).Returns(true);

            // Act
            var actualTransferId = service.Object.CreateTransfer(Guid.NewGuid(), sourceAccountId, destAccountId, amount, description);

            // Assert
            service.Verify(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId), Times.Once());
            context.Verify(c => c.CreateTransfer(sourceAccountId, destAccountId, description, amount), Times.Once());
            Assert.AreEqual(transferId, actualTransferId);
        }

        [SetUp]
        public void SetUp()
        {
            this.container = new UnityContainer();
        }
    }
}