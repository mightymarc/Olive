// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountControllerTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the AccountControllerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.Account;

    [TestFixture]
    public class AccountControllerTests : ControllerTestBase<AccountController>
    {
        private static readonly Random Random = new Random();

        [Test]
        public void AccountDetailsContainsTransferHistory()
        {
            var accountId = Random.Next(1, 10000000);
            var sessionId = Guid.NewGuid();
            var accountDisplayName = Guid.NewGuid().ToString().Substring(10);

            this.sessionMock.SetupGet(s => s.HasSession).Returns(true);
            this.sessionMock.SetupGet(s => s.SessionId).Returns(sessionId);

            var tranfers = new List<GetAccountTransfersTransfer> {
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
            this.serviceMock.Setup(s => s.GetAccount(sessionId, accountId)).Returns(
                new GetAccountAccount { DisplayName = accountDisplayName });

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

        [Test]
        public void CreateAccountPostRedirectsWhenNotLoggedIn()
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/Account/Create");
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var model = new CreateViewModel { CurrencyId = "BTC", DisplayName = "abc" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Create(model);

            // Assert
            Assert.AreEqual("Login", actionResult.RouteValues["action"]);
            Assert.AreEqual("User", actionResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Create", actionResult.RouteValues["returnUrl"]);
        }

        [Test]
        public void CreateAccountRedirectsWhenNotLoggedIn()
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/Account/Create");
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var model = new CreateViewModel { CurrencyId = "BTC", DisplayName = "abc" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Create(model);

            // Assert
            Assert.AreEqual("Login", actionResult.RouteValues["action"]);
            Assert.AreEqual("User", actionResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Create", actionResult.RouteValues["returnUrl"]);
        }

        [Test]
        public void CreateAccountSuccessTest()
        {
            // Arrange
            var model = new CreateViewModel { CurrencyId = "USD", DisplayName = string.Empty };

            var newAccountId = Random.Next(1, 10000000);
            var sessionId = this.SetupHasSession();
            this.serviceMock.Setup(s => s.CreateCurrentAccount(sessionId, model.CurrencyId, model.DisplayName)).Returns(
                newAccountId);
            var target = this.CreateController();

            // Act
            var result = (RedirectToRouteResult)target.Create(model);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void CreateAccountWithoutArguments()
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/Account/Create");
            this.SetupHasSession();

            // Act
            var actionResult = (ViewResult)controller.Create();

            // Assert
            Assert.AreEqual(string.Empty, actionResult.ViewName);
            Assert.IsNotNull(actionResult.Model);
        }

        [Test]
        public void EditAccountWithModel()
        {
            // Arrange
            var controller = this.CreateController();
            this.SetupHasSession();

            var viewModel = new EditViewModel { AccountId = 100, DisplayName = "Display name" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Edit(viewModel.AccountId, viewModel);

            // Assert
            Assert.AreEqual(null, actionResult.RouteValues["controller"]);
            Assert.AreEqual(null, actionResult.RouteValues["action"]);
        }

        [Test]
        public void EditAccountWithModelRedirectsWhenNotLoggedIn()
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/Account/Edit/100");
            this.sessionMock.Setup(s => s.HasSession).Returns(false);

            var viewModel = new EditViewModel { AccountId = 100, DisplayName = "Display name" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Edit(viewModel.AccountId, viewModel);

            // Assert
            Assert.AreEqual("Login", actionResult.RouteValues["action"]);
            Assert.AreEqual("User", actionResult.RouteValues["controller"]);
            Assert.AreEqual("/Account/Edit/100", actionResult.RouteValues["returnUrl"]);
        }

        [Test]
        public void EditAccountWithoutModel()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void EditAccountWithoutModelRedirectsWhenNotLoggedIn()
        {
            Assert.Inconclusive();
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
                            CurrencyId = "ABC", 
                            DisplayName = string.Empty
                        }, 
                    new AccountOverviewAccount
                        {
                            AccountId = Random.Next(1, 10000000), 
                            Balance = (decimal)Random.Next(1, 10000000) / 100, 
                            CurrencyId = "NOK", 
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
        public void IndexWhenNotAuthenticatedRedirectsToLogin()
        {
            // Arrange
            var controller = this.CreateController(relativePath: "/Account");
            this.sessionMock.SetupGet(s => s.HasSession).Returns(false);
            var model = new CreateViewModel { CurrencyId = "PPUSD", DisplayName = "abc" };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual("User", actionResult.RouteValues["controller"]);
            Assert.AreEqual("Login", actionResult.RouteValues["action"]);
            Assert.AreEqual("/Account", actionResult.RouteValues["returnUrl"]);
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }
    }
}