// -----------------------------------------------------------------------
// <copyright file="AccountControllerTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.Account;

    [TestFixture]
    public class AccountControllerTests
    {
        private IUnityContainer container = new UnityContainer();

        private Mock<ISiteSession> sessionMock;

        private Mock<IWebService> serviceMock;

        private Mock<HttpContextBase> httpContextMock;

        [SetUp]
        public void SetUp()
        {
            this.sessionMock = new Mock<ISiteSession>();
            this.serviceMock = new Mock<IWebService>();
            this.httpContextMock = new Mock<HttpContextBase>();

            // Register mocks with unity
            this.container.RegisterInstance(this.sessionMock.Object);
            this.container.RegisterInstance(this.serviceMock.Object);
            this.container.RegisterInstance(this.httpContextMock.Object);
        }

        [Test]
        public void CannotRegisterWhenLoggedIn()
        {
            Assert.Inconclusive("Not implemented.");
        }

        [Test]
        public void RegisterActionLogsInAndRegistersOnSuccess()
        {
            var email = "valid@email.info";
            var password = Guid.NewGuid().ToString().Substring(0, 10);
            var sessionId = Guid.NewGuid();
            var userId = Random.Next(1, 1000);

            // Mocks services
            var service = this.container.Resolve<IWebService>();
            var session = this.container.Resolve<ISiteSession>();

            // Expectations
            this.sessionMock.SetupGet(m => m.HasSession).Returns(false);
            this.sessionMock.SetupSet(s => s.SessionId = sessionId);
            this.serviceMock.Setup(s => s.CreateUser(email, password)).Returns(userId);
            this.serviceMock.Setup(s => s.CreateSession(email, password)).Returns(sessionId);

            var target = new AccountController();
            this.container.BuildUp(target);

            var actionResult = target.Register(email, password);

            // Assert
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), actionResult);
            var redirectActionResult = (RedirectToRouteResult)actionResult;
            Assert.False(redirectActionResult.Permanent);
            Assert.AreEqual("Account", redirectActionResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectActionResult.RouteValues["action"]);
        }

        [Test]
        public void IndexContainsAccountOverviewViewData()
        {
            var target = new AccountController();
            var sessionId = Guid.NewGuid();

            this.sessionMock.SetupGet(s => s.HasSession).Returns(true);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);

            var mockServiceResult = new AccountOverview
                {
                    new AccountOverviewAccount
                        {
                            AccountId = Random.Next(1, 10000000),
                            Balance = (decimal)Random.Next(1, 10000000) / 100,
                            CurrencyShortName = "ABC",
                            DisplayName = string.Empty
                        },
                    new AccountOverviewAccount
                        {
                            AccountId = Random.Next(1, 10000000),
                            Balance = (decimal)Random.Next(1, 10000000) / 100,
                            CurrencyShortName = "NOK",
                            DisplayName = string.Empty
                        }
                };

            this.serviceMock.Setup(s => s.GetAccountOverview(sessionId)).Returns(mockServiceResult);

            var actionResult = target.Index();
            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;
            Assert.IsInstanceOf(typeof(IndexViewModel), viewResult.Model);

            var viewModel = (IndexViewModel)viewResult.Model;
            Assert.AreEqual(mockServiceResult.Count, viewModel.Accounts.Count);
            mockServiceResult.ForEach(x => Assert.Contains(x, viewModel.Accounts));
        }

        [Test]
        public void AccountDetailsContainsTransferHistory()
        {
            var accountId = Random.Next(1, 10000000);
            var sessionId = Guid.NewGuid();
            var accountDisplayName = Guid.NewGuid().ToString().Substring(10);

            this.sessionMock.SetupGet(s => s.HasSession).Returns(true);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);

            var tranfers = new List<GetAccountTransfersTransfer>()
                {
                    new GetAccountTransfersTransfer
                        {
                            Amount = (decimal)Random.Next(1, 10000000) / 100,
                            CurrencyShortName = "USD",
                            DestAccountId = Random.Next(1, 1000000) * 2,
                            SourceAccountId = (Random.Next(1, 1000000) * 2) + 1,
                            TransferId = Guid.NewGuid()
                        },
                    new GetAccountTransfersTransfer
                        {
                            Amount = (decimal)Random.Next(1, 10000000) / 100,
                            CurrencyShortName = "USD",
                            DestAccountId = Random.Next(1, 1000000) * 2,
                            SourceAccountId = (Random.Next(1, 1000000) * 2) + 1,
                            TransferId = Guid.NewGuid()
                        }
                };

            this.serviceMock.Setup(s => s.GetAccountTransfers(sessionId, accountId)).Returns(tranfers);
            this.serviceMock.Setup(s => s.GetAccount(sessionId, accountId)).Returns(new GetAccountAccount { DisplayName = accountDisplayName });

            var target = new AccountController();
            this.container.BuildUp(target);

            var actionResult = target.Details(accountId);
            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;
            Assert.IsInstanceOf(typeof(DetailsViewModel), viewResult.Model);

            var viewModel = (DetailsViewModel)viewResult.Model;
            Assert.AreEqual(tranfers.Count, viewModel.Transfers.Count);
            tranfers.ForEach(x => Assert.Contains(x, viewModel.Transfers));
            Assert.AreEqual(accountDisplayName, viewModel.AccountDisplayName);
        }

        private static readonly Random Random = new Random();

        [Test]
        public void CreateAccountSuccessTest()
        {
            var displayName = Guid.NewGuid().ToString().Substring(0, 10);
            var mockAccountId = Random.Next(1, 100000000);
            var mockSessionId = Guid.NewGuid();
            var currencyId = Random.Next(1, 100);
            var target = new AccountController();
            this.container.BuildUp(target);

            this.sessionMock.SetupGet(s => s.HasSession).Returns(true);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(mockSessionId);

            this.serviceMock.Setup(s => s.CreateAccount(mockSessionId, currencyId, displayName)).Returns(mockAccountId);

            var actionResult = target.CreateAccount(currencyId, displayName);

            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;

            Assert.AreEqual(viewResult.ViewName, "Index");
            Assert.AreEqual(mockAccountId, viewResult.TempData["CreatedAccountId"]);
        }

        [Test]
        public void CreateAccountRedirectsWhenNotLoggedIn()
        {
            var mockRequest = new Mock<HttpRequestBase>();

            this.httpContextMock.SetupGet(c => c.Request).Returns(mockRequest.Object);
            mockRequest.SetupGet(r => r.RawUrl).Returns("/Account/Index");

            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);

            var target = new AccountController();
            this.container.BuildUp(target);

            var actionResult = target.CreateAccount(123, "fghjkl");

            Assert.IsInstanceOf(typeof(RedirectToRouteResult), actionResult);

            var redirectResult = (RedirectToRouteResult)actionResult;

            // Can't really expect the account name to be persisted here.
            Assert.AreEqual("Auth", redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Index", redirectResult.RouteValues["returnUrl"]);
        }

        [Test]
        public void RegisterStaysOnPageWhenEmailIsIncorrect()
        {
            var target = new AccountController();
            var email = "noat";
            var password = "password";

            var actionResult = target.Register(email, password);
            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;
            Assert.AreEqual("Auth", viewResult.ViewName);

            Assert.IsInstanceOf(typeof(AuthViewModel), viewResult.Model);
            var authViewModel = (AuthViewModel)viewResult.Model;

            Assert.AreEqual(password, authViewModel.RegisterPassword);
            Assert.AreEqual(email, authViewModel.RegisterEmail);
            Assert.AreEqual(1, target.ModelState.Count);
        }

        [Test]
        public void RegisterStaysOnPageWhenPasswordIsEmpty()
        {
            var target = new AccountController();

            var actionResult = target.Register("email@email.com", string.Empty);
            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;
            Assert.AreEqual("Auth", viewResult.ViewName);

            Assert.IsInstanceOf(typeof(AuthViewModel), viewResult.Model);
            var authViewModel = (AuthViewModel)viewResult.Model;

            Assert.AreEqual(string.Empty, authViewModel.RegisterPassword);
            Assert.AreEqual(1, target.ModelState.Count);
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
            var email = "andy@andy.com";
            var password = "ghjk!dflkjh$gf";
            var expectedSessionId = Guid.NewGuid();

            var siteSessionPersister = this.container.Resolve<ISiteSession>();
            var mockService = this.container.Resolve<IWebService>();

            var controller = new AccountController();
            this.container.BuildUp(controller);

            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(Guid.Empty);
            this.serviceMock.Setup(s => s.CreateSession(email, password)).Returns(expectedSessionId);
            this.sessionMock.SetupSet(s => s.SessionId = expectedSessionId);

            // Act
            var actionResult = controller.Login(email, password);

            // Assert
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), actionResult);
            var redirectActionResult = (RedirectToRouteResult)actionResult;
            Assert.False(redirectActionResult.Permanent);
            Assert.AreEqual("Account", redirectActionResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectActionResult.RouteValues["action"]);
        }

        [Test]
        public void LoginActionRedirectsWithRedirectUrlOnSuccess()
        {
            // Setup
            var email = "bob@jane.com";
            var password = "ghjk!dflkjh$gf";
            var expectedSessionId = Guid.NewGuid();
            var redirectUrl = "/SomeController/Index";

            var siteSessionPersister = this.container.Resolve<ISiteSession>();
            var mockService = this.container.Resolve<IWebService>();

            var controller = new AccountController();
            this.container.BuildUp(controller);

            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(Guid.Empty);
            this.serviceMock.Setup(s => s.CreateSession(email, password)).Returns(expectedSessionId);
            this.sessionMock.SetupSet(s => s.SessionId = expectedSessionId);

            // Act
            var actionResult = controller.Login(email, password, redirectUrl);

            // Assert
            Assert.IsInstanceOf(typeof(RedirectResult), actionResult);
            var redirectActionResult = (RedirectResult)actionResult;
            Assert.False(redirectActionResult.Permanent);
            Assert.AreEqual(redirectActionResult.Url, redirectUrl);
        }

        [Test]
        public void ReturnUrlIsValidatedToNotBeAbsolute()
        {
            Assert.Inconclusive("Not implemented.");

            // Expected behavior is to ignore the bad redirect url.
        }

        [Test]
        public void IndexWhenNotAuthenticatedRedirectsToLogin()
        {
            var mockRequest = new Mock<HttpRequestBase>();
            this.httpContextMock.SetupGet(s => s.Request).Returns(mockRequest.Object);
            mockRequest.SetupGet(s => s.RawUrl).Returns("/Account/Index");

            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);

            var controller = new AccountController();
            this.container.BuildUp(controller);

            var actionResult = controller.Index();

            Assert.IsInstanceOf(typeof(RedirectToRouteResult), actionResult);

            var redirectResult = (RedirectToRouteResult)actionResult;

            Assert.AreEqual("Auth", redirectResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Index", redirectResult.RouteValues["returnUrl"]);
        }

        [Test]
        public void LoginActionStaysWithErrorMessageOnFailure()
        {
            // Setup
            var email = "bob@bob.com";
            var password = "ghjk!dflkjh$gf";

            var siteSessionPersister = this.container.Resolve<ISiteSession>();
            var mockService = this.container.Resolve<IWebService>();

            var controller = new AccountController();
            this.container.BuildUp(controller);

            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(Guid.Empty);
            this.serviceMock.Setup(s => s.CreateSession(email, password)).Throws(new FaultException<AuthenticationFault>(new AuthenticationFault()));

            // Act
            var actionResult = controller.Login(email, password);

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
