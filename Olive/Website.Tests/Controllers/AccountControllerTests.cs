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
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.Account;

    [TestFixture]
    public class AccountControllerTests : ControllerTestBase<AccountController>
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void IndexContainsAccountOverviewViewData()
        {
            var target = new AccountController();
            this.container.BuildUp(target);

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

            this.serviceMock.Setup(s => s.GetAccounts(sessionId)).Returns(mockServiceResult).Verifiable();

            var actionResult = target.Index();

            this.serviceMock.Verify(s => s.GetAccounts(sessionId), Times.Once());

            Assert.IsInstanceOf(typeof(ViewResult), actionResult);

            var viewResult = (ViewResult)actionResult;
            Assert.IsInstanceOf(typeof(IndexViewModel), viewResult.Model);

            var viewModel = (IndexViewModel)viewResult.Model;
            Assert.IsNotNull(viewModel.Accounts);

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
            // Arrange
            var model = new CreateAccountViewModel
            {
                CurrencyId = Random.Next(1, 10000),
                DisplayName = string.Empty
            };

            var newAccountId = Random.Next(1, 10000000);
            var sessionId = this.SetupHasSession();
            this.serviceMock.Setup(s => s.CreateAccount(sessionId, model.CurrencyId, model.DisplayName)).Returns(newAccountId);
            var target = this.CreateController();

            // Act
            var result = (RedirectToRouteResult)target.CreateAccount(model);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void CreateAccountRedirectsWhenNotLoggedIn()
        {
            // Arrange
            var controller = this.CreateController();
            Mock.Get(controller.Request).SetupGet(r => r.RawUrl).Returns("/Account/Index");
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var model = new CreateAccountViewModel { CurrencyId = 1, DisplayName = "abc" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.CreateAccount(model);
            
            // Assert
            Assert.AreEqual("Login", actionResult.RouteValues["action"]);
            Assert.AreEqual("User", actionResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Index", actionResult.RouteValues["returnUrl"]);
        }


        [Test]
        public void IndexWhenNotAuthenticatedRedirectsToLogin()
        {
            // Arrange
            var controller = this.CreateController();
            Mock.Get(controller.Request).SetupGet(r => r.RawUrl).Returns("/Account/Index");
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var model = new CreateAccountViewModel { CurrencyId = 1, DisplayName = "abc" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual("Login", actionResult.RouteValues["action"]);
            Assert.AreEqual("User", actionResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Index", actionResult.RouteValues["returnUrl"]);
        }
    }
}
