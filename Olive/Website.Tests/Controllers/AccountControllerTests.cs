// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountControllerTests.cs" company="Olive">
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

    /// <summary>
    /// The account controller tests.
    /// </summary>
    [TestFixture]
    public class AccountControllerTests : ControllerTestBase<AccountController>
    {
        #region Constants and Fields

        /// <summary>
        /// The random.
        /// </summary>
        private static readonly Random Random = new Random();

        #endregion

        #region Public Methods

        /// <summary>
        /// The account details contains transfer history.
        /// </summary>
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

        /// <summary>
        /// The create account post redirects when not logged in.
        /// </summary>
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

        /// <summary>
        /// The create account redirects when not logged in.
        /// </summary>
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

        /// <summary>
        /// The create account success test.
        /// </summary>
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

        /// <summary>
        /// The create account without arguments.
        /// </summary>
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

        /// <summary>
        /// The edit account with model.
        /// </summary>
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

        /// <summary>
        /// The edit account with model redirects when not logged in.
        /// </summary>
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

        /// <summary>
        /// The edit account without model.
        /// </summary>
        [Test]
        public void EditAccountWithoutModel()
        {
            Assert.Inconclusive();
        }

        /// <summary>
        /// The edit account without model redirects when not logged in.
        /// </summary>
        [Test]
        public void EditAccountWithoutModelRedirectsWhenNotLoggedIn()
        {
            Assert.Inconclusive();
        }

        /// <summary>
        /// The index contains account overview view data.
        /// </summary>
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

        /// <summary>
        /// The index when not authenticated redirects to login.
        /// </summary>
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

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        #endregion
    }
}