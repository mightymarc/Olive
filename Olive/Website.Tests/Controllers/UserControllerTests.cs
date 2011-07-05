// -----------------------------------------------------------------------
// <copyright file="UserControllerTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.User;

    [TestFixture]
    public class UserControllerTests : ControllerTestBase<UserController>
    {
        [Test]
        public void CannotRegisterWhenLoggedIn()
        {
            Assert.Inconclusive("Not implemented.");
        }

        [Test]
        public void RegisterActionLogsInAndRegistersOnSuccess()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void RegisterStaysOnPageWhenEmailIsIncorrect()
        {
            Assert.Inconclusive();
        }

        [Test]
        [TestCase("valid@email.com")]
        public void RegisterStaysOnPageWhenPasswordIsEmpty(string email)
        {
            // Arrange
            var target = this.CreateController();

            // Act
            var model = new RegisterViewModel { Email = email, Password = string.Empty, ConfirmPassword = string.Empty };
            var viewResult = (ViewResult)target.Register(model);

            // Assert
            Assert.Inconclusive("Not implemented.");
        }

        [Test]
        public void LoginViewProcessesReturnUrl()
        {
            // Arrange
            var controller = this.CreateController();

            // Act
            var view = (ViewResult)controller.Login("/Account/Index");

            // Assert
            Assert.AreEqual("/Account/Index", view.ViewData["ReturnUrl"]);
        }

        [Test]
        public void LoginActionRedirectsOnSuccess()
        {
            // Arrange
            var controller = this.CreateController();
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var sessionId = Guid.NewGuid();
            this.sessionMock.SetupSet(s => s.SessionId = sessionId);

            var email = "valid@email.com";
            var password = "password";

            this.serviceMock.Setup(s => s.CreateSession(email, password)).Returns(sessionId);

            // Act
            var redirectResult = (RedirectToRouteResult)controller.Login(new LoginViewModel { Email = email, Password = password });

            // Assert
            this.serviceMock.Verify(s => s.CreateSession(email, password), Times.Once());
        }

        [Test]
        public void LoginActionRedirectsWithRedirectUrlOnSuccess()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void ReturnUrlIsValidatedToNotBeAbsolute()
        {
            Assert.Inconclusive("Not implemented.");

            // Expected behavior is to ignore the bad redirect url.
        }

        [Test]
        public void LoginActionStaysWithErrorMessageOnFailure()
        {
            Assert.Inconclusive();
        }
    }
}
