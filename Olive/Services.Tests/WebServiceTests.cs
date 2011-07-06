// -----------------------------------------------------------------------
// <copyright file="WebServiceTests.cs" company="Olive">
//  [Copyright]
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Services.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.DataAccess;
    using Olive.DataAccess.Tests;
    using Olive.Services;

    [TestFixture]
    public sealed class WebServiceTests
    {
        private IUnityContainer container;

        [SetUp]
        public void SetUp()
        {
            this.container = new UnityContainer();
        }

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
        public void CreateSessionWitInvalidEmailThrowsException()
        {
            var service = new WebService();

            Assert.Throws<ArgumentException>(() => service.CreateSession("@bademail.com", "password"));
        }

        [Test]
        public void CreateSessionWithWrongPasswordThrowsAuthenticationFault()
        {
            var email = "email@pass.com";
            var passwordHash = "passwordHash";

            var service = new WebService() { Container = this.container };
            
            var context = new Mock<IOliveContext>().Object;
            this.container.RegisterInstance(context);

            var mockUsers = new MockDbSet<User>() { new User() { Email = email, PasswordSalt = "salt" } };
            Mock.Get(context).SetupProperty(c => c.Users, mockUsers);

            Mock.Get(context).Setup(c => c.CreateSession(email, It.IsAny<string>())).Throws(new AuthenticationException());

            // Mock ICrypto.
            var mockCrypto = new Mock<ICrypto>();
            mockCrypto.Setup(c => c.CreateSalt()).Returns("salt");
            mockCrypto.Setup(c => c.GenerateHash(It.IsAny<string>(), It.IsAny<string>())).Returns(passwordHash);
            this.container.RegisterInstance(mockCrypto.Object);

            Assert.Throws<AuthenticationException>(() => service.CreateSession(email, passwordHash));
            Mock.Get(context).Verify(c => c.CreateSession(email, It.IsAny<string>()), Times.Once());
        }

        [Test]
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

            var service = new WebService() { Container = this.container };

            Mock.Get(context).Setup(c => c.CreateSession(email, It.IsAny<string>())).Returns(sessionId);

            Assert.AreEqual(sessionId, service.CreateSession(email, passwordHash));
            Mock.Get(context).Verify(c => c.CreateSession(email, It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void CreateSessionWithUnknownEmailThrowsAuthenticationFault()
        {
            var email = "email@pass.com";
            var passwordHash = "passwordHash";

            var context = new MockOliveContext();
            this.container.RegisterInstance<IOliveContext>(context);
            var service = new WebService() { Container = this.container };

            Assert.Throws<AuthenticationException>(() => service.CreateSession(email, passwordHash));
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
                string.Format(CultureInfo.CurrentCulture, "E-mail '{0}' should not have been allowed to register.", email));
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
        public void CreateUserWithNullPasswordThrowsException()
        {
            // Arrange
            var service = new WebService { Container = this.container };

            // Act and assert
            Assert.Throws<ArgumentException>(() => service.CreateUser("a@b.com", null));
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
        public void CreateUserWithAlreadyRegisteredEmailThrowsException()
        {
            // Arrange
            var email = "user@email.com";
            var password = "passwordHash";
            var context = new MockOliveContext();
            this.container.RegisterInstance<IOliveContext>(context);

            context.Users.Add(new User { Email = "USER@EMAIL.com", PasswordHash = "hash", PasswordSalt = "salt", UserId = 100 });

            // Act
            var service = new WebService { Container = this.container };

            // Assert
            Assert.Throws<EmailAlreadyRegisteredException>(() => service.CreateUser(email, password));
        }
    }
}
