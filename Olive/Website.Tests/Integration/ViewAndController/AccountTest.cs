// -----------------------------------------------------------------------
// <copyright file="AccountTest.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests.Integration.ViewAndController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using HtmlAgilityPack;

    using Moq;

    using MvcIntegrationTestFramework;
    using MvcIntegrationTestFramework.Hosting;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.Helpers;

    using MockInjectionFilterProvider = Olive.Website.Tests.MockInjectionFilterProvider;

    public class AccountTest
    {
        [Test]
        public void AccountWhenNotLoggedInRedirectsToLogin()
        {
            AppHost.Simulate("Website").Start(bs =>
            {
                // Arrange
                Mock<ISiteSession> sessionMock = null;

                FilterProviders.Providers.Add(
                        new MockInjectionFilterProvider(
                            fc =>
                            {
                                sessionMock = new Mock<ISiteSession>();
                                sessionMock.SetupGet(s => s.HasSession).Returns(false);
                                ((SiteController)fc.Controller).SessionPersister = sessionMock.Object;
                                return false;
                            }));

                // Act
                var result = bs.Get("Account");

                // Assert
                sessionMock.VerifyGet(s => s.HasSession);

                Assert.IsInstanceOf(typeof(RedirectToRouteResult), result.ResultExecutedContext.Result);
                var redirectResult = (RedirectToRouteResult)result.ResultExecutedContext.Result;
                Assert.AreEqual("Login", redirectResult.RouteValues["action"]);
                Assert.AreEqual("User", redirectResult.RouteValues["controller"]);
            });
        }

        [Test]
        public void AccountIndexTest()
        {
            AppHost.Simulate("Website").Start(bs =>
            {
                // Arrange
                var sessionId = Guid.NewGuid();
                Mock<ISiteSession> sessionMock = null;
                Mock<IWebService> serviceMock = null;
                var accountOverview = new AccountOverview
                        {
                            new AccountOverviewAccount
                                {
                                    AccountId = 100, Balance = 200m, CurrencyId = "BTC", DisplayName = null 
                                },
                            new AccountOverviewAccount
                                {
                                    AccountId = 101, Balance = 50.5m, CurrencyId = "USD", DisplayName = "Coins" 
                                }
                        };

                FilterProviders.Providers.Add(
                    new MockInjectionFilterProvider(
                        fc =>
                        {
                            sessionMock = new Mock<ISiteSession>();
                            sessionMock.SetupGet(s => s.HasSession).Returns(true);
                            sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);
                            ((SiteController)fc.Controller).SessionPersister = sessionMock.Object;

                            serviceMock = new Mock<IWebService>();
                            serviceMock.Setup(s => s.GetAccounts(sessionId)).Returns(accountOverview);
                            ((SiteController)fc.Controller).Service = serviceMock.Object;

                            return false;
                        }));

                // Act
                var result = bs.Get("Account");

                // Assert
                sessionMock.VerifyGet(s => s.HasSession);
                sessionMock.Verify(s => s.SessionId);
                serviceMock.Verify(s => s.GetAccounts(sessionId));

                Assert.IsInstanceOf(typeof(ViewResult), result.ResultExecutedContext.Result);
                var viewResult = (ViewResult)result.ResultExecutedContext.Result;

                var document = new HtmlDocument();
                document.LoadHtml(result.ResponseText);

                Assert.IsNotNull(
                    document.DocumentNode.SelectSingleNode(
                        string.Format("//a[@href='/Account/Edit/{0}']", accountOverview[0].AccountId)));

                Assert.IsNotNull(
                    document.DocumentNode.SelectSingleNode(
                        string.Format("//a[@href='/Account/Edit/{0}']", accountOverview[1].AccountId)));

                Assert.IsNotNull(
                    document.DocumentNode.SelectSingleNode(
                        string.Format("//a[@href='/Account/Transfer/?SourceAccountId={0}']", accountOverview[0].AccountId)));

                Assert.IsNotNull(
                    document.DocumentNode.SelectSingleNode(
                        string.Format("//a[@href='/Account/Transfer/?SourceAccountId={0}']", accountOverview[1].AccountId)));
            });
        }

        [Test]
        public void AccountCreateCurrentTest()
        {
            AppHost.Simulate("Website").Start(bs =>
            {
                // Arrange
                Mock<ISiteSession> sessionMock = null;
                Mock<IWebService> serviceMock = null;
                var sessionId = Guid.NewGuid();
                var currencyId = "USD";
                var displayName = string.Empty;
                var accountId = UnitTestHelper.Random.Next(1, int.MaxValue);

                FilterProviders.Providers.Add(
                    new MockInjectionFilterProvider(
                        fc =>
                        {
                            sessionMock = new Mock<ISiteSession>();
                            sessionMock.SetupGet(s => s.HasSession).Returns(true);
                            sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);
                            ((SiteController)fc.Controller).SessionPersister = sessionMock.Object;

                            serviceMock = new Mock<IWebService>();
                            serviceMock.Setup(s => s.CreateCurrentAccount(sessionId, currencyId, displayName)).Returns(accountId);
                            ((SiteController)fc.Controller).Service = serviceMock.Object;

                            return false;
                        }));

                // Act
                var result = bs.Post("/Account/Create", new { CurrencyId = currencyId, DisplayName = displayName });

                // Assert
                sessionMock.VerifyGet(s => s.HasSession);
                sessionMock.Verify(s => s.SessionId);
                serviceMock.Verify(s => s.CreateCurrentAccount(sessionId, currencyId, displayName));

                Assert.IsInstanceOf(typeof(RedirectToRouteResult), result.ResultExecutedContext.Result);
                var redirectResult = (RedirectToRouteResult)result.ResultExecutedContext.Result;
                Assert.AreEqual(string.Empty, redirectResult.RouteValues["action"]);
                Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
            });
        }
    }
}
