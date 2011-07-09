// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Olive">
//   
// </copyright>
// <summary>
//   The account controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Web.Mvc;

    using Olive.Website.ViewModels.Account;

    /// <summary>
    ///   The account controller.
    /// </summary>
    public class AccountController : SiteController
    {
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            Contract.Requires<InvalidOperationException>(this.Service != null);
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null);
            Contract.Requires<ArgumentNullException>(model != null, "model");

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            if (this.ModelState.IsValid)
            {
                var accountId = this.Service.CreateCurrentAccount(
                    this.SessionPersister.SessionId, model.CurrencyId, model.DisplayName);

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        public ActionResult Create()
        {
            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            return this.View(new CreateViewModel { Currencies = this.CurrencyCache.Currencies });
        }

        public ActionResult Details(int accountId)
        {
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null, "this.SessionPersister == null");
            Contract.Requires<InvalidOperationException>(this.Service != null);

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var viewModel = new DetailsViewModel
                {
                    AccountDisplayName = this.Service.GetAccount(this.SessionPersister.SessionId, accountId).DisplayName, 
                    Transfers = this.Service.GetAccountTransfers(this.SessionPersister.SessionId, accountId)
                };

            return this.View("Details", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int accountId, EditViewModel model)
        {
            Contract.Requires<InvalidOperationException>(this.Service != null);
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null);
            Contract.Requires<ArgumentNullException>(model != null, "model");

            model.AccountId = accountId;

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.Service.EditCurrentAccount(this.SessionPersister.SessionId, model.AccountId, model.DisplayName);
            return this.RedirectToAction("", "Account");
        }

        [HttpGet]
        public ActionResult Edit(int accountId)
        {
            Contract.Requires<InvalidOperationException>(this.Service != null);
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null);
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var account = this.Service.GetAccount(this.SessionPersister.SessionId, accountId);

            var viewModel = new EditViewModel { AccountId = accountId, DisplayName = account.DisplayName };

            return this.View(viewModel);
        }

        /// <summary>
        ///   The index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Contract.Requires<ArgumentNullException>(this.SessionPersister != null, "this.SessionPersister");
            Contract.Requires<ArgumentNullException>(this.Service != null, "this.Service");

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var accounts = this.Service.GetAccounts(this.SessionPersister.SessionId);

            var viewModel = new IndexViewModel { Accounts = accounts };

            return View("Index", viewModel);
        }
    }
}