﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditTests.cs" company="Olive">
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
//   Defines the EditTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Web.Mvc;

    using HtmlAgilityPack;

    using Moq;

    using MvcIntegrationTestFramework;
    using MvcIntegrationTestFramework.Browsing;
    using MvcIntegrationTestFramework.Hosting;

    using Microsoft.Practices.Unity;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.Helpers;
    using Olive.Website.ViewModels.Account;
    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    /// <summary>
    /// The edit tests.
    /// </summary>
    [TestFixture]
    public class EditTests
    {
        /// <summary>
        /// The with view model renders without exceptions 1.
        /// </summary>
        [Test]
        public void WithViewModelRendersWithoutExceptions()
        {
            var view = new Edit();

            var viewModel = new EditViewModel { AccountId = 123, DisplayName = null };

            var html = view.RenderAsHtml(viewModel);
        }

        /// <summary>
        /// The without view model renders without exceptions.
        /// </summary>
        [Test]
        public void WithoutViewModelRendersWithoutExceptions()
        {
            var view = new Edit();
            var viewModel = new EditViewModel { AccountId = 612345 };

            var html = view.RenderAsHtml(viewModel);

            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//form"), "Form missing");
            Assert.IsNotNull(
                html.DocumentNode.SelectSingleNode("//input[@type='text' and @name='DisplayName' and @value='']"), 
                "DisplayName textbox missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='submit']"), "Submit button missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//a[@href='/Account']"), "Cancel link missing");
        }

        [Test]
        public void IntegrationWithoutSessionAndViewModelRedirects()
        {
            var appHost = AppHost.Simulate("Website");
            appHost.Start(browsingSession =>
                {
                    var requestResult = browsingSession.Get("Account/Edit/1234");

                    Assert.AreEqual((int)HttpStatusCode.Redirect, requestResult.Response.StatusCode); 
                });
        }

        [Test]
        public void IntegrationWithoutSessionWithModelRedirectsx()
        {
            var appHost = AppHost.Simulate("Website");
            appHost.Start(browsingSession =>
                {
                    var formData =
                        NameValueCollectionConversions.ConvertFromObject(
                            new { AccountId = 1234, DisplayName = "Display name" });

                    var requestResult = browsingSession.Post("Account/Edit/1234", formData);

                    Assert.AreEqual((int)HttpStatusCode.Redirect, requestResult.Response.StatusCode);
                });
        }

        [Test]
        public void IntegrationWithoutSessionWithModelRedirects()
        {
            var appHost = AppHost.Simulate("Website");
            appHost.Start(browsingSession =>
            {
                var formData =
                    NameValueCollectionConversions.ConvertFromObject(
                        new { AccountId = 1234, DisplayName = "Display name" });

                var requestResult = browsingSession.Post("Account/Edit/1234", formData);

                Assert.AreEqual((int)HttpStatusCode.Redirect, requestResult.Response.StatusCode);
            });
        }

        public class MockInjectionFilterProvider : IFilterProvider
        {
            public Func<ActionExecutingContext, bool> Action { get; set; }

            public class ActionFilter : ActionFilterAttribute
            {
                private bool disabled;

                public override void OnActionExecuting(ActionExecutingContext filterContext)
                {
                    if (this.disabled)
                    {
                        return;
                    }

                    if (!this.Action(filterContext))
                    {
                        this.disabled = true;
                    }
                }

                public Func<ActionExecutingContext, bool> Action { get; set; }
            }

            public MockInjectionFilterProvider(Func<ActionExecutingContext, bool> action)
            {
                this.Action = action;
            }

            public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            {
                yield return new Filter(new ActionFilter { Action = this.Action }, FilterScope.Action, null);
            }
        }

        [Test]
        public void IntegrationLoginCreateAccountAndEditIt()
        {
            var email = "user@pass.com";
            var password = "password";
            var sessionId = Guid.NewGuid();

            var appHost = AppHost.Simulate("Website", null);

            appHost.Start(
                browsingSession =>
                    {
                        // Go to /Account and expect to be redirected.
                        var requestResult = browsingSession.Get("Account");
                        Assert.IsInstanceOf(typeof(RedirectToRouteResult), requestResult.ResultExecutedContext.Result);

                        // Go to login page
                        requestResult = browsingSession.Get("/User/Login");
                        Assert.IsInstanceOf(typeof(ViewResult), requestResult.ResultExecutedContext.Result);
                        var viewResult = (ViewResult)requestResult.ResultExecutedContext.Result;
                        Assert.AreEqual("Login", viewResult.ViewName);

                        var document = new HtmlDocument();
                        document.LoadHtml(requestResult.ResponseText);

                        // Log in using random credentials
                        var formData = new { Email = email, Password = password };

                        FilterProviders.Providers.Add(
                            new MockInjectionFilterProvider(
                                fc =>
                                    {
                                        var serviceMock = new Mock<IWebService>();
                                        serviceMock.Setup(s => s.CreateSession(email, password)).Returns(sessionId);
                                        ((SiteController)fc.Controller).Service = serviceMock.Object;
                                        return false;
                                    }));

                        requestResult = browsingSession.Post("/User/Login", formData);

                        Assert.IsNull(requestResult.ActionExecutedContext.Exception);
                        Assert.AreEqual(
                            (int)HttpStatusCode.Redirect,
                            requestResult.Response.StatusCode,
                            "Wrong return code from login action.");
                        Assert.AreEqual("/Account", requestResult.Response.RedirectLocation);

                        // Go to create account
                        FilterProviders.Providers.Add(
                            new MockInjectionFilterProvider(
                                fc =>
                                {
                                    var sessionMock = new Mock<ISiteSession>();
                                    sessionMock.Setup(s => s.HasSession).Returns(true);
                                    ////sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);
                                    ((SiteController)fc.Controller).SessionPersister = sessionMock.Object;
                                    return false;
                                }));

                        requestResult = browsingSession.Get("/Account/Create");
                        Assert.IsInstanceOf(typeof(ViewResult), requestResult.ResultExecutedContext.Result);
                        viewResult = (ViewResult)requestResult.ResultExecutedContext.Result;
                        Assert.AreEqual("Create", viewResult.ViewName);
                    });
        }
    }
}