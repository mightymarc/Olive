// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerTestBase.cs" company="Olive">
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

    /// <summary>
    /// The controller test base.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class ControllerTestBase<T>
        where T : Controller, new()
    {
        /// <summary>
        ///   The container.
        /// </summary>
        protected IUnityContainer container = new UnityContainer();

        /// <summary>
        ///   The currency cache.
        /// </summary>
        protected Mock<ICurrencyCache> currencyCache;

        /// <summary>
        ///   The http context mock.
        /// </summary>
        protected Mock<HttpContextBase> httpContextMock;

        /// <summary>
        ///   The service mock.
        /// </summary>
        protected Mock<IWebService> serviceMock;

        /// <summary>
        ///   The session mock.
        /// </summary>
        protected Mock<ISiteSession> sessionMock;

        /// <summary>
        /// The set up.
        /// </summary>
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
            this.container.RegisterInstance<IFaultFactory>(new FaultFactory());
        }

        /// <summary>
        /// The assert view model.
        /// </summary>
        /// <param name="actionResult">
        /// The action result.
        /// </param>
        /// <typeparam name="VM">
        /// </typeparam>
        /// <returns>
        /// </returns>
        protected VM AssertViewModel<VM>(ActionResult actionResult)
        {
            var viewResult = this.AssertViewResult(actionResult);

            Assert.IsInstanceOf(typeof(VM), viewResult.Model);
            var viewModel = (VM)viewResult.Model;

            return viewModel;
        }

        /// <summary>
        /// The assert view result.
        /// </summary>
        /// <param name="actionResult">
        /// The action result.
        /// </param>
        /// <returns>
        /// </returns>
        protected ViewResult AssertViewResult(ActionResult actionResult)
        {
            Contract.Requires<ArgumentNullException>(actionResult != null, "actionResult");
            Contract.Ensures(Contract.Result<ViewResult>() != null);

            Assert.IsInstanceOf(typeof(ViewResult), actionResult);
            return (ViewResult)actionResult;
        }

        /// <summary>
        /// The create controller.
        /// </summary>
        /// <param name="buildUp">
        /// The build up.
        /// </param>
        /// <param name="relativePath">
        /// The relative path.
        /// </param>
        /// <returns>
        /// </returns>
        protected T CreateController(bool buildUp = true, string relativePath = null)
        {
            var controller = new T();

            if (buildUp)
            {
                this.container.BuildUp(controller);
            }

            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var contextBase = relativePath == null
                                  ? MvcMockHelpers.FakeHttpContext()
                                  : MvcMockHelpers.FakeHttpContext("~" + relativePath);
            controller.ControllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(contextBase, new RouteData()), routes);

            Mock.Get(controller.Request).SetupGet(s => s.Url).Returns(
                new Uri("http://localhost" + relativePath, UriKind.Absolute));

            return controller;
        }

        /// <summary>
        /// The setup has session.
        /// </summary>
        /// <returns>
        /// </returns>
        protected Guid SetupHasSession()
        {
            var sessionId = Guid.NewGuid();
            this.sessionMock.SetupGet(m => m.HasSession).Returns(true);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);
            return sessionId;
        }
    }
}