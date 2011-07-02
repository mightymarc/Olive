// -----------------------------------------------------------------------
// <copyright file="AccountControllerTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Website.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    using NMock2;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.Account;

    [TestFixture]
    public class AccountControllerTests
    {
        private IUnityContainer container;
        private Mockery mockery = new Mockery();

        [SetUp]
        public void SetUp()
        {
            this.container = new UnityContainer();
        }

        [Test]
        public void AuthActionReturnsAuthView()
        {
            var controller = new AccountController();

            var actionResult = controller.Auth();

            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;
            Assert.AreEqual("Auth", viewResult.ViewName);

            Assert.IsInstanceOf(typeof(AuthViewModel), viewResult.Model);
            var viewModel = (AuthViewModel)viewResult.Model;

            Assert.False(viewModel.LoginError);
        }

        [Test]
        public void LoginActionRedirectsOnSuccess()
        {
            // Setup
            var userId = 500000;
            var password = "ghjk!dflkjh$gf";
            var expectedSessionId = Guid.NewGuid();

            // Register mocks with unity
            this.container.RegisterInstance<ISiteSessionPersister>(this.mockery.NewMock<ISiteSessionPersister>());
            this.container.RegisterInstance<IWebService>(this.mockery.NewMock<IWebService>());

            var siteSessionPersister = this.container.Resolve<ISiteSessionPersister>();
            var mockService = this.container.Resolve<IWebService>();

            var controller = new AccountController();
            this.container.BuildUp(controller);

            Expect.Once.On(siteSessionPersister).GetProperty("HasSession").Will(Return.Value(false));
            Expect.On(siteSessionPersister).GetProperty("SessionId").Will(Return.Value(Guid.Empty));
            Expect.Once.On(mockService).Method("CreateSession").With(userId, password).Will(
                Return.Value(expectedSessionId));
            Expect.Once.On(siteSessionPersister).SetProperty("SessionId").To(expectedSessionId);

            // Act
            var actionResult = controller.Login(userId, password);

            // Assert
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), actionResult);
            var redirectActionResult = (RedirectToRouteResult)actionResult;
            Assert.False(redirectActionResult.Permanent);
            Assert.AreEqual("Account", redirectActionResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectActionResult.RouteValues["action"]);
        }

        [Test]
        public void IndexWhenNotAuthenticatedRedirectsToLogin()
        {
            this.container.RegisterInstance(this.mockery.NewMock<ISiteSessionPersister>());
            this.container.RegisterInstance(this.mockery.NewMock<IWebService>());
            this.container.RegisterInstance(this.mockery.NewMock<HttpContextBase>());

            var siteSessionPersister = this.container.Resolve<ISiteSessionPersister>();
            var mockContext = this.container.Resolve<HttpContextBase>();
            var mockRequest = this.mockery.NewMock<HttpRequestBase>();
            Expect.On(mockContext).GetProperty("Request").Will(Return.Value(mockRequest));
            Expect.On(mockRequest).GetProperty("RawUrl").Will(Return.Value("/Account/Index"));

            Expect.On(siteSessionPersister).GetProperty("HasSession").Will(Return.Value(false));

            var controller = new AccountController();
            this.container.BuildUp(controller);

            var actionResult = controller.Index();

            Assert.IsInstanceOf(typeof(RedirectToRouteResult), actionResult);

            var redirectResult = (RedirectToRouteResult)actionResult;

            Assert.AreEqual("Auth", redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Index", redirectResult.RouteValues["redirectUrl"]);
        }

        [Test]
        public void LoginActionStaysWithErrorMessageOnFailure()
        {
            // Setup
            var userId = 500000;
            var password = "ghjk!dflkjh$gf";

            // Register mocks with unity
            this.container.RegisterInstance<ISiteSessionPersister>(this.mockery.NewMock<ISiteSessionPersister>());
            this.container.RegisterInstance<IWebService>(this.mockery.NewMock<IWebService>());

            var siteSessionPersister = this.container.Resolve<ISiteSessionPersister>();
            var mockService = this.container.Resolve<IWebService>();

            var controller = new AccountController();
            this.container.BuildUp(controller);

            Expect.Once.On(siteSessionPersister).GetProperty("HasSession").Will(Return.Value(false));
            Expect.On(siteSessionPersister).GetProperty("SessionId").Will(Return.Value(Guid.Empty));
            Expect.Once.On(mockService).Method("CreateSession").With(userId, password).Will(Throw.Exception(new FaultException<AuthenticationFault>(new AuthenticationFault())));

            // Act
            var actionResult = controller.Login(userId, password);

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), actionResult);
            var viewResult = (ViewResult)actionResult;

            Assert.IsInstanceOf(typeof(AuthViewModel), viewResult.Model);
            Assert.AreEqual("Auth", viewResult.ViewName);

            var viewModel = (AuthViewModel)viewResult.Model;

            Assert.True(viewModel.LoginError);
        }
    }
}
