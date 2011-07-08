// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerTestBase.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the ControllerTestBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Helpers;

    public abstract class ControllerTestBase<T>
        where T : Controller, new()
    {
        protected IUnityContainer container = new UnityContainer();

        protected Mock<ICurrencyCache> currencyCache;

        protected Mock<HttpContextBase> httpContextMock;

        protected Mock<IWebService> serviceMock;

        protected Mock<ISiteSession> sessionMock;

        [SetUp]
        public virtual void SetUp()
        {
            this.sessionMock = new Mock<ISiteSession>();
            this.serviceMock = new Mock<IWebService>();
            this.httpContextMock = new Mock<HttpContextBase>();
            this.sessionMock = new Mock<ISiteSession>();
            this.currencyCache = new Mock<ICurrencyCache>();
            this.currencyCache.Setup(c => c.Currencies).Returns(new List<string> { "USD", "BTC" });

            // Register mocks with unity
            this.container.RegisterInstance(this.sessionMock.Object);
            this.container.RegisterInstance(this.currencyCache.Object);
            this.container.RegisterInstance(this.serviceMock.Object);
            this.container.RegisterInstance(this.httpContextMock.Object);
        }

        protected VM AssertViewModel<VM>(ActionResult actionResult)
        {
            var viewResult = this.AssertViewResult(actionResult);

            Assert.IsInstanceOf(typeof(VM), viewResult.Model);
            var viewModel = (VM)viewResult.Model;

            return viewModel;
        }

        protected ViewResult AssertViewResult(ActionResult actionResult)
        {
            Contract.Requires<ArgumentNullException>(actionResult != null, "actionResult");
            Contract.Ensures(Contract.Result<ViewResult>() != null);

            Assert.IsInstanceOf(typeof(ViewResult), actionResult);
            return (ViewResult)actionResult;
        }

        protected T CreateController(bool buildUp = true, string relativePath = null)
        {
            var controller = new T();
            this.container.BuildUp(controller);

            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var contextBase = MvcMockHelpers.FakeHttpContext(); ////new MockHttpContext();
            controller.ControllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(contextBase, new RouteData()), routes);

            Mock.Get(controller.Request).SetupGet(s => s.Url).Returns(
                new Uri("http://localhost/" + relativePath, UriKind.Absolute));

            return controller;
        }

        protected Guid SetupHasSession()
        {
            var sessionId = Guid.NewGuid();
            this.sessionMock.SetupGet(m => m.HasSession).Returns(true);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);
            return sessionId;
        }
    }
}