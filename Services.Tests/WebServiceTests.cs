// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebServiceTests.cs" company="Olive">
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
//   Defines the WebServiceTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.DataAccess;
    using Olive.DataAccess.Domain;
    using Olive.DataAccess.Tests;

    /// <summary>
    /// The web service tests.
    /// </summary>
    [TestFixture]
    public sealed class WebServiceTests
    {
        /// <summary>
        ///   The create transfer with bad arguments throws exception cases.
        /// </summary>
        private static object[] createTransferWithBadArgumentsThrowsExceptionCases = {
                                                                                         new object[] { 0, 1, 100m, "cc" }
                                                                                         , 
                                                                                         new object[]
                                                                                             {
                                                                                                1, 1, 101m, string.Empty 
                                                                                             }, 
                                                                                         new object[]
                                                                                             {
                                                                                                -1, 1, 102m, "cc" 
                                                                                             }, 
                                                                                         new object[]
                                                                                             {
                                                                                                1, 1, -103m, string.Empty 
                                                                                             }
                                                                                         , 
                                                                                         new object[]
                                                                                             {
                                                                                                1, -1, 104m, "cc" 
                                                                                             }, 
                                                                                         new object[] { 1, 1, 0m, "cc" }, 
                                                                                         new object[] { 1, 1, 6m, "cc" }, 
                                                                                         new object[]
                                                                                             {
                                                                                                1, 0, 105m, "des" 
                                                                                             }
                                                                                     };

        /// <summary>
        ///   The edit account does not throw exception cases.
        /// </summary>
        private static object[] editAccountDoesNotThrowExceptionCases = {
                                                                            new object[] { Guid.NewGuid(), 123, "Name" }, 
                                                                            new object[] { Guid.NewGuid(), 1, null }, 
                                                                            new object[] { Guid.NewGuid(), 333434, null }
                                                                        };

        /// <summary>
        ///   The edit account with bad arguments throws exception cases.
        /// </summary>
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
                                                                                              Guid.NewGuid(), -1, 
                                                                                              string.Empty
                                                                                          }, 
                                                                                      new object[]
                                                                                          {
                                                                                             Guid.NewGuid(), -1, null 
                                                                                          }
                                                                                  };

        /// <summary>
        ///   The container.
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        ///   The fault factory.
        /// </summary>
        private IFaultFactory faultFactory;

        /// <summary>
        /// The create account with good credentials does not throw exception.
        /// </summary>
        /// <param name="currencyId">
        /// The currency id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCase("USD", null)]
        [TestCase("USD", "")]
        [TestCase("BTC", "Name of account to create")]
        public void CreateAccountWithGoodCredentialsDoesNotThrowException(string currencyId, string displayName)
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            var service = new ClientService { Container = this.container };
            var sessionId = Guid.NewGuid();

            var expectedAccountId = 53;

            var userId = 512;

            context.Setup(c => c.VerifySession(sessionId)).Returns(userId);

            // Create account should convert empty strings to null because it's not appropriate in the database.
            context.Setup(
                c => c.CreateCurrentAccount(userId, currencyId, displayName == string.Empty ? null : displayName)).
                Returns(expectedAccountId);

            // Act
            var actualAccountId = service.CreateCurrentAccount(sessionId, currencyId, displayName);

            // Assert
            Assert.AreEqual(expectedAccountId, actualAccountId);
        }

        /// <summary>
        /// The create current account with bad arguments throws exception.
        /// </summary>
        /// <param name="currencyId">
        /// The currency id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "name")]
        [TestCase("", "name")]
        public void CreateCurrentAccountWithBadArgumentsThrowsException(string currencyId, string displayName)
        {
            var service = new ClientService { Container = this.container };
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

        /// <summary>
        /// The create current account without credentials throws exception.
        /// </summary>
        [Test]
        public void CreateCurrentAccountWithoutCredentialsThrowsException()
        {
            // Arrange
            var service = new ClientService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateCurrentAccount(Guid.Empty, "a@b.com", null));
        }

        /// <summary>
        /// The create session wit invalid email throws exception.
        /// </summary>
        [Test]
        public void CreateSessionWitInvalidEmailThrowsException()
        {
            var service = new ClientService();

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

        /// <summary>
        /// The create session with null email throws exception.
        /// </summary>
        [Test]
        public void CreateSessionWithNullEmailThrowsException()
        {
            var service = new ClientService();
            Assert.Throws<ArgumentException>(() => service.CreateSession(null, "password"));
        }

        /// <summary>
        /// The create session with null password throws exception.
        /// </summary>
        [Test]
        public void CreateSessionWithNullPasswordThrowsException()
        {
            var service = new ClientService();

            Assert.Throws<ArgumentException>(() => service.CreateSession("valid@email.com", null));
        }

        /// <summary>
        /// The create session with unknown email throws authentication fault.
        /// </summary>
        [Test]
        public void CreateSessionWithUnknownEmailThrowsAuthenticationFault()
        {
            var email = "email@pass.com";
            var passwordHash = "passwordHash";

            var context = new MockOliveContext();
            this.container.RegisterInstance<IOliveContext>(context);
            var service = this.GetMockWebService();

            Assert.Throws<FaultException>(() => service.CreateSession(email, passwordHash));
        }

        /// <summary>
        /// The create transfer success does not throw exception.
        /// </summary>
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
            context.Setup(c => c.CreateTransfer(sourceAccountId, destAccountId, description, amount)).Returns(
                transferId);
            this.container.RegisterInstance(context.Object);

            var service = this.GetMockWebService();
            Mock.Get(service).Setup(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId)).Returns(true);

            // Act
            var actualTransferId = service.CreateTransfer(
                Guid.NewGuid(), sourceAccountId, destAccountId, amount, description);

            // Assert
            Mock.Get(service).Verify(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId), Times.Once());
            context.Verify(c => c.CreateTransfer(sourceAccountId, destAccountId, description, amount), Times.Once());
            Assert.AreEqual(transferId, actualTransferId);
        }

        /// <summary>
        /// The create transfer with bad arguments throws exception.
        /// </summary>
        /// <param name="sourceAccountId">
        /// The source account id.
        /// </param>
        /// <param name="destAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        [Test]
        [TestCaseSource("createTransferWithBadArgumentsThrowsExceptionCases")]
        public void CreateTransferWithBadArgumentsThrowsException(
            int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            context.Setup(c => c.VerifySession(It.IsAny<Guid>())).Returns(1);

            IBankService service = new ClientService { Container = this.container };

            try
            {
                service.CreateTransfer(Guid.NewGuid(), sourceAccountId, destAccountId, amount, description);
            }
            catch (ArgumentException)
            {
                return;

                // Assert.Pass(string.Format("Source={0}; Dest={1}; Amount={2}; Desc={3}", sourceAccountId, destAccountId, amount, description));
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }

            Assert.Fail("Unsupported case did not throw exception.");
        }

        /// <summary>
        /// The create transfer without access throws exception.
        /// </summary>
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

            var service = this.GetMockWebService();
            Mock.Get(service).Setup(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId)).Returns(false);

            // Act
            Assert.Throws<FaultException>(
                () => service.CreateTransfer(Guid.NewGuid(), sourceAccountId, destAccountId, amount, description));

            // Assert
            Mock.Get(service).Verify(s => s.UserCanWithdrawFromAccount(userId, sourceAccountId), Times.Once());
        }

        /// <summary>
        /// The create transfer without authentication throws exception.
        /// </summary>
        [Test]
        public void CreateTransferWithoutAuthenticationThrowsException()
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            IBankService service = this.GetMockWebService();

            var sesionId = Guid.NewGuid();
            var sourceAccountId = 1;
            var destAccountId = 2;
            var amount = 100.5m;
            var description = "Because";

            context.Setup(c => c.VerifySession(sesionId)).Throws(new SessionDoesNotExistException());

            Assert.Throws<FaultException>(
                () => service.CreateTransfer(sesionId, sourceAccountId, destAccountId, amount, description));

            // Assert
            context.Verify(c => c.VerifySession(sesionId), Times.Once());
        }

        /// <summary>
        /// The create user does not throw exception.
        /// </summary>
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

            var service = new ClientService { Container = this.container };

            // Act
            service.CreateUser(email, password, 0);

            // Assert
            Assert.IsNotNull(contextMock.Object.Users.SingleOrDefault(u => u.Email == email));
        }

        /// <summary>
        /// The create user with already registered email throws exception.
        /// </summary>
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
            var service = this.GetMockWebService();

            // Assert
            try
            {
                service.CreateUser(email, password, 0);
            }
            catch (FaultException fe)
            {
                if (fe.Code.Name == FaultFactory.EmailAlreadyRegisteredFaultCode.Name)
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }

        /// <summary>
        /// The create user with bad email format throws exception.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
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
            var service = new ClientService { Container = this.container };

            // Assert
            Assert.Throws<ArgumentException>(
                () => service.CreateUser(email, password, 0), 
                string.Format(
                    CultureInfo.CurrentCulture, "E-mail '{0}' should not have been allowed to register.", email));
        }

        /// <summary>
        /// The create user with null email throws exception.
        /// </summary>
        [Test]
        public void CreateUserWithNullEmailThrowsException()
        {
            // Arrange
            var service = new ClientService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser(null, "password", 0));
        }

        /// <summary>
        /// The create user with null password throws exception.
        /// </summary>
        [Test]
        public void CreateUserWithNullPasswordThrowsException()
        {
            // Arrange
            var service = new ClientService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser("a@b.com", null, 0));
        }

        /// <summary>
        /// The create user with too short password throws exception.
        /// </summary>
        [Test]
        public void CreateUserWithTooShortPasswordThrowsException()
        {
            // Arrange
            var service = new ClientService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser("a@b.c", "ab", 0));
        }

        /// <summary>
        /// The edit current account does not throw exception.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCaseSource("editAccountDoesNotThrowExceptionCases")]
        public void EditCurrentAccountDoesNotThrowException(Guid sessionId, int accountId, string displayName)
        {
            // Arrange
            var context = new Mock<IOliveContext>();
            this.container.RegisterInstance(context.Object);

            var userId = 512;

            var mockService = new Mock<ClientService> { CallBase = true };
            mockService.SetupGet(s => s.Container).Returns(this.container);
            var service = (IClientService)mockService.Object;
            mockService.Setup(s => s.UserCanEditAccount(userId, accountId)).Returns(true);

            context.Setup(c => c.VerifySession(sessionId)).Returns(userId);

            // Create account should convert empty strings to null because it's not appropriate in the database.
            context.Setup(c => c.EditCurrentAccount(accountId, displayName == string.Empty ? null : displayName));

            // Act
            service.EditCurrentAccount(sessionId, accountId, displayName);

            // Assert
            mockService.Verify(s => s.UserCanEditAccount(userId, accountId));
            context.Verify(c => c.VerifySession(sessionId));
            context.Verify(c => c.EditCurrentAccount(accountId, displayName == string.Empty ? null : displayName));
        }

        /// <summary>
        /// The edit current account with bad arguments throws exception.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        [Test]
        [TestCaseSource("editAccountWithBadArgumentsThrowsExceptionCases")]
        public void EditCurrentAccountWithBadArgumentsThrowsException(Guid sessionId, int accountId, string displayName)
        {
            // Arrange
            var service = new ClientService();

            // Act and assert
            try
            {
                service.EditCurrentAccount(sessionId, accountId, displayName);
            }
            catch (ArgumentException)
            {
                Assert.Pass("ArgumentException was thrown as expected.");
            }

            Assert.Fail("ArgumentException was expected to be thrown.");
        }

        /// <summary>
        /// The edit current account with invalid session id throws exception.
        /// </summary>
        [Test]
        public void EditCurrentAccountWithInvalidSessionIdThrowsException()
        {
            // Arrange
            var sessionId = Guid.NewGuid();

            var mockContext = new Mock<IOliveContext>();
            this.container.RegisterInstance(mockContext.Object);
            mockContext.Setup(c => c.VerifySession(sessionId)).Throws(new AuthenticationException());

            var service = this.GetMockWebService();

            // Act and assert
            try
            {
                service.EditCurrentAccount(sessionId, 700, null);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (FaultException fe)
            {
                if (fe.Code.Name == FaultFactory.SessionDoesNotExistFaultCode.Name)
                {
                    Assert.Pass("Exception was thrown as expected.");
                }

                throw;
            }
        }

        /// <summary>
        /// The edit current account without having access test.
        /// </summary>
        [Test]
        public void EditCurrentAccountWithoutHavingAccessTest()
        {
            // Arrange
            var userId = 1;
            var sessionId = Guid.NewGuid();
            var accountId = 2;
            var displayName = default(string);

            var mockContext = new Mock<IOliveContext>();
            mockContext.Setup(c => c.VerifySession(sessionId)).Returns(userId);
            this.container.RegisterInstance(mockContext.Object);

            var service = this.GetMockWebService();
            Mock.Get(service).Setup(s => s.UserCanEditAccount(userId, accountId)).Returns(false);

            // Act and assert
            try
            {
                Mock.Get(service).Object.EditCurrentAccount(sessionId, accountId, displayName);
            }
            catch (FaultException fe)
            {
                if (fe.Code.Name == FaultFactory.UnauthorizedAccountEditFaultCode.Name)
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }

        /// <summary>
        /// The get account does not throw exception.
        /// </summary>
        [Test]
        public void GetAccountDoesNotThrowException()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var accountId = 1;
            var userId = 3;
            var displayName = default(string);
            var currencyId = "BTC";

            var mockContext = new Mock<IOliveContext>();
            mockContext.Setup(c => c.VerifySession(sessionId)).Returns(userId);

            var mockDbSet = new Mock<DataAccess.Tests.MockDbSet<Account>>();
            mockDbSet.CallBase = true;
            var account = new Account
                { AccountId = accountId, AccountType = "Current", CurrencyId = currencyId, DisplayName = displayName };
            mockDbSet.Object.Add(account);
            mockDbSet.Setup(db => db.Find(accountId)).Returns(account);

            mockContext.SetupGet(c => c.Accounts).Returns(mockDbSet.Object);
            this.container.RegisterInstance(mockContext.Object);

            var service = this.GetMockWebService();
            Mock.Get(service).Setup(s => s.UserCanViewAccount(userId, accountId)).Returns(true);

            // Act
            service.GetAccount(sessionId, accountId);

            // Assert
            mockContext.VerifyGet(c => c.Accounts, Times.Once());
            mockContext.Verify(c => c.VerifySession(sessionId), Times.Once());
            Mock.Get(service).Verify(s => s.UserCanViewAccount(userId, accountId), Times.Once());
        }

        /// <summary>
        /// The get account throws exception for non existing account.
        /// </summary>
        [Test]
        public void GetAccountThrowsExceptionForNonExistingAccount()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var accountId = 1;
            var userId = 3;
            ////var displayName = default(string);
            ////var currencyId = "BTC";

            var mockContext = new Mock<IOliveContext>();
            mockContext.Setup(c => c.VerifySession(sessionId)).Returns(userId);
            this.container.RegisterInstance(mockContext.Object);

            var service = this.GetMockWebService();
            Mock.Get(service).Setup(s => s.UserCanViewAccount(userId, accountId)).Returns(false);

            // Act
            Assert.Throws<FaultException>(() => service.GetAccount(sessionId, accountId));

            // Assert
            Mock.Get(service).Verify(s => s.UserCanViewAccount(userId, accountId), Times.Once());
        }

        /// <summary>
        /// The get account throws exception when user does not have access.
        /// </summary>
        [Test]
        public void GetAccountThrowsExceptionWhenUserDoesNotHaveAccess()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var accountId = 100;
            var userId = 150;

            var mockContext = new Mock<IOliveContext>();
            mockContext.Setup(c => c.VerifySession(sessionId)).Returns(userId);
            this.container.RegisterInstance(mockContext.Object);

            var service = this.GetMockWebService();
            Mock.Get(service).Setup(s => s.UserCanViewAccount(userId, accountId)).Returns(false);

            try
            {
                service.GetAccount(sessionId, accountId);
            }
            catch (FaultException fe)
            {
                if (fe.Code.Name == FaultFactory.UnauthorizedAccountAccessFaultCode.Name)
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }

        /// <summary>
        /// The get currencies test.
        /// </summary>
        [Test]
        public void GetCurrenciesTest()
        {
            // Arrange
            var service = new ClientService { Container = this.container };

            var currencies =
                new[] { "USD", "BTC", "ARG", "PPUSD" }.Select(x => new Currency { CurrencyId = x }).ToList();
            var mockDbSet = new Mock<MockDbSet<Currency>>() { CallBase = true };
            currencies.ForEach(x => mockDbSet.Object.Add(x));

            var mockContext = new Mock<IOliveContext>();
            mockContext.SetupGet(c => c.Currencies).Returns(mockDbSet.Object);
            this.container.RegisterInstance(mockContext.Object);

            var result = service.GetCurrencies();

            Assert.AreEqual(currencies.Count, result.Count);
            mockContext.VerifyGet(c => c.Currencies, Times.Once());
        }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.container = new UnityContainer();
            this.faultFactory = new FaultFactory();
            this.container.RegisterInstance(this.faultFactory);
        }

        /// <summary>
        /// The get mock web service.
        /// </summary>
        /// <returns>
        /// </returns>
        private ClientService GetMockWebService()
        {
            var mockService = new Mock<ClientService> { CallBase = true };
            mockService.Object.Container = this.container;
            mockService.Object.FaultFactory = this.faultFactory;

            return mockService.Object;
        }
    }
}