// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserControllerTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the UserControllerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Controllers
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web.Mvc;

    using Moq;

    using NUnit.Framework;

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
            var redirectResult =
                (RedirectToRouteResult)controller.Login(new LoginViewModel { Email = email, Password = password });

            // Assert
            this.serviceMock.Verify(s => s.CreateSession(email, password), Times.Once());
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
        }

        [Test]
        public void LoginActionStaysWithErrorMessageOnFailure()
        {
            Assert.Inconclusive();
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
        public void RegisterActionLogsInAndRegistersOnSuccess()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void RegisterEmptyActionReturnsDefaultViewModel()
        {
            // Arrange
            var controller = this.CreateController();

            // Act
            var viewModel = controller.Register();

            // Assert
            Assert.IsNotNull(viewModel.Model);
        }

        [Test]
        public void RegisterStaysOnPageWhenEmailIsIncorrect()
        {
            // Arrange
            var controller = this.CreateController();
            var model = new RegisterViewModel
                {
                   Email = "invalid", Password = "properpassword123", ConfirmPassword = "properpassword123" 
                };
            this.CreateModelStateFromModel(controller, model);

            // Act
            var viewResult = (ViewResult)controller.Register(model);

            // Assert
            Assert.AreEqual(string.Empty, viewResult.ViewName);
        }

        [Test]
        [TestCase("valid@email.com")]
        public void RegisterStaysOnPageWhenPasswordIsEmpty(string email)
        {
            // Arrange
            var controller = this.CreateController();
            var model = new RegisterViewModel { Email = email, Password = string.Empty, ConfirmPassword = string.Empty };
            this.CreateModelStateFromModel(controller, model);

            // Act
            var viewResult = (ViewResult)controller.Register(model);

            // Assert
        }

        /// <summary>
        ///   Returns the URL is validated to not be absolute or outside the site.
        ///   When the url is bad, the redirect should be changed to the default.
        /// </summary>
        /// <param name = "badReturnUrl">The bad return URL.</param>
        [Test]
        [TestCase("http://www.otherside.com/")]
        [TestCase("javascript:DoSomething();")]
        [TestCase("ftp://otherprotocol.com")]
        public void ReturnUrlIsValidatedToNotBeAbsolute(string badReturnUrl)
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/User/Login");
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var sessionId = Guid.NewGuid();
            this.sessionMock.SetupSet(s => s.SessionId = sessionId);

            var email = "valid@email.com";
            var password = "password";

            this.serviceMock.Setup(s => s.CreateSession(email, password)).Returns(sessionId);

            // Act
            var redirectResult =
                (RedirectToRouteResult)
                controller.Login(new LoginViewModel { Email = email, Password = password, ReturnUrl = badReturnUrl });

            // Assert
            this.serviceMock.Verify(s => s.CreateSession(email, password), Times.Once());
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
        }

        [Test]
        public void UsingUrlHelperOnControllerDoesNotCauseIssues()
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/User/Login");

            // Act
            var isLocal = controller.Url.IsLocalUrl("http://www.someurl.com");

            // Assert
            Assert.False(isLocal);
        }

        private void CreateModelStateFromModel(Controller controller, object model)
        {
            var modelBinder = new ModelBindingContext
                {
                    ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType()), 
                    ValueProvider =
                        new NameValueCollectionValueProvider(new NameValueCollection(), CultureInfo.InvariantCulture)
                };
            var binder = new DefaultModelBinder().BindModel(new ControllerContext(), modelBinder);
            controller.ModelState.Clear();
            controller.ModelState.Merge(modelBinder.ModelState);
        }
    }
}