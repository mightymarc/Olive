// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserControllerTests.cs" company="Olive">
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

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.User;

    /// <summary>
    /// The user controller tests.
    /// </summary>
    [TestFixture]
    public class UserControllerTests : ControllerTestBase<UserController>
    {
        /// <summary>
        /// The cannot register when logged in.
        /// </summary>
        [Test]
        public void CannotViewRegisterWhenLoggedIn()
        {
            // Arrange
            var controller = this.CreateController();
            this.sessionMock.SetupGet(s => s.HasSession).Returns(true);

            // Act
            Assert.Throws<InvalidOperationException>(() => controller.Register());
        }

        /// <summary>
        /// The login action redirects on success.
        /// </summary>
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
            Assert.AreEqual(string.Empty, redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
        }

        /// <summary>
        /// The login action stays with error message on failure.
        /// </summary>
        [Test]
        public void LoginActionStaysWithErrorMessageOnFailure()
        {
            // Arrange
            var controller = this.CreateController();
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var sessionId = Guid.NewGuid();
            this.sessionMock.SetupSet(s => s.SessionId = sessionId);

            var email = "valid@email.com";
            var password = "password";

            var faultFactory = new FaultFactory();

            this.serviceMock.Setup(s => s.CreateSession(email, password)).Throws(
                faultFactory.CreateUnrecognizedCredentialsException(email));

            // Act
            var viewResult = (ViewResult)controller.Login(new LoginViewModel { Email = email, Password = password });

            // Assert
            this.serviceMock.Verify(s => s.CreateSession(email, password), Times.Once());
            Assert.AreEqual(string.Empty, viewResult.ViewName);
        }

        /// <summary>
        /// The login view processes return url.
        /// </summary>
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

        /// <summary>
        /// The register action logs in and registers on success.
        /// </summary>
        [Test]
        public void RegisterActionLogsInAndRegistersOnSuccess()
        {
            // Arrange
            var controller = this.CreateController();

            var userId = UnitTestHelper.Random.Next(1, int.MaxValue);
            var sessionId = Guid.NewGuid();
            var email = UnitTestHelper.GetRandomEmail();
            var password = UnitTestHelper.GetRandomDisplayName();

            this.serviceMock.Setup(s => s.CreateSession(email, password)).Returns(sessionId);
            this.serviceMock.Setup(s => s.CreateUser(email, password));

            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            this.sessionMock.SetupSet(s => s.SessionId = sessionId);

            // Act
            var actionResult = controller.Register(
                new RegisterViewModel { ConfirmPassword = password, Email = email, Password = password });

            // Assert
            var redirectToRouteResult = (RedirectToRouteResult)actionResult;
            Assert.AreEqual(string.Empty, redirectToRouteResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectToRouteResult.RouteValues["controller"]);

            this.serviceMock.Verify(s => s.CreateSession(email, password), Times.Once());
            this.serviceMock.Verify(s => s.CreateUser(email, password), Times.Once());

            this.sessionMock.VerifyGet(s => s.HasSession, Times.Once());
            this.sessionMock.VerifySet(s => s.SessionId = sessionId, Times.Once());
        }

        /// <summary>
        /// The register empty action returns default view model.
        /// </summary>
        [Test]
        public void RegisterEmptyActionReturnsDefaultViewModel()
        {
            // Arrange
            var controller = this.CreateController();

            // Act
            var viewResult = (ViewResult)controller.Register();

            // Assert
            Assert.IsNotNull(viewResult.Model);
        }

        /// <summary>
        /// The register stays on page when email is incorrect.
        /// </summary>
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

        /// <summary>
        /// The register stays on page when password is empty.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
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
        /// Returns the URL is validated to not be absolute or outside the site.
        ///   When the url is bad, the redirect should be changed to the default.
        /// </summary>
        /// <param name="badReturnUrl">
        /// The bad return URL.
        /// </param>
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
            Assert.AreEqual(string.Empty, redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
        }

        /// <summary>
        /// The using url helper on controller does not cause issues.
        /// </summary>
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

        /// <summary>
        /// The create model state from model.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
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