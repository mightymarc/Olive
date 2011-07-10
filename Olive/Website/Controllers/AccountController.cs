﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Olive">
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
    /// The account controller.
    /// </summary>
    public class AccountController : SiteController
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// </returns>
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

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Create()
        {
            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            return this.View(new CreateViewModel { Currencies = this.CurrencyCache.Currencies });
        }

        /// <summary>
        /// The details.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
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

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// </returns>
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
            return this.RedirectToAction(string.Empty, "Account");
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
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
        /// The index.
        /// </summary>
        /// <returns>
        /// </returns>
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