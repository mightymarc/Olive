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
    using System.Web.Mvc;

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
            return this.View("Auth");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Index()
        {
            if (!SiteSessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var accounts = ServiceHelper.Instance.GetAccounts(SiteSessionPersister.SessionId);

            return View(accounts);
        }

        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="userId">The identifier of the user trying to authenticate.</param>
        /// <param name="password">The password to use for authentication.</param>
        /// <returns>A redirect to the account index if the login was successful.</returns>
        [HttpPost]
        public ActionResult Login(int userId, string password)
        {
            SiteSessionPersister.SessionId = ServiceHelper.Instance.CreateSession(userId, password);

            return this.RedirectToAction("Index", "Account");
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
            var userId = ServiceHelper.Instance.CreateUser(password);
            SiteSessionPersister.SessionId = ServiceHelper.Instance.CreateSession(userId, password);

            return this.RedirectToAction("Index", "Account");
        }
    }
}