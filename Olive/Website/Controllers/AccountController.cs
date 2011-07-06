// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="">
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
    using System.ServiceModel;
    using System.Web.Mvc;

    using Olive.Services;
    using Olive.Website.ViewModels.Account;

    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : SiteController
    {
        /// <summary>
        /// The index.
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

            var viewModel = new IndexViewModel() { Accounts = accounts };

            return View("Index", viewModel);
        }



        [HttpPost]
        public ActionResult CreateAccount(CreateAccountViewModel model)
        {
            Contract.Requires<InvalidOperationException>(this.Service != null);
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null);
            Contract.Requires<ArgumentNullException>(model != null, "model");

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            if (ModelState.IsValid)
            {
                var accountId = this.Service.CreateAccount(this.SessionPersister.SessionId, model.CurrencyId, model.DisplayName);

                return RedirectToAction("Index");
            }

            return this.View(model);
        }

        public ActionResult Details(int accountId)
        {
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null, "this.SessionPersister == null");

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
    }
}