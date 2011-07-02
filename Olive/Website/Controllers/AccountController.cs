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
        // GET: /Account/

        /// <summary>
        /// The auth.
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Auth()
        {
            return this.View("Auth", new AuthViewModel());
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Contract.Requires<ArgumentNullException>(this.SessionPersister != null, "this.SessionPersister");
            Contract.Requires<ArgumentNullException>(this.Service != null, "this.Service");
            Contract.Requires<ArgumentNullException>(this.Context != null, "this.Context != null");

            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var accounts = this.Service.GetAccounts(this.SessionPersister.SessionId);

            return View(accounts);
        }

        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="userId">The identifier of the user trying to authenticate.</param>
        /// <param name="password">The password to use for authentication.</param>
        /// <returns>A redirect to the account index if the login was successful.</returns>
        [HttpPost]
        public ActionResult Login(int userId, string password, string returnUrl = null)
        {
            Contract.Requires<ArgumentNullException>(this.SessionPersister != null, "this.SessionPersister");
            Contract.Requires<ArgumentNullException>(this.Service != null, "this.Service");

            try
            {
                var sessionId = this.Service.CreateSession(userId, password);
                this.SessionPersister.SessionId = sessionId;

                if (returnUrl == null)
                {
                    return this.RedirectToAction("Index", "Account");
                }

                return new RedirectResult(returnUrl);
            }
            catch (FaultException<AuthenticationFault>)
            {
                return this.View("Auth", new AuthViewModel() { LoginError = true });
            }
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public ActionResult Register(string password)
        {
            var userId = this.Service.CreateUser(password);
            this.SessionPersister.SessionId = this.Service.CreateSession(userId, password);

            return this.RedirectToAction("Index", "Account");
        }
    }
}