// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the UserController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;
    using System.Web.Mvc;

    using Olive.Services;
    using Olive.Website.ViewModels.User;

    public class UserController : SiteController
    {
        /// <summary>
        ///   The login view (GET).
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            this.ViewData.Add("ReturnUrl", returnUrl);

            return this.View();
        }

        /// <summary>
        ///   Logins the specified user id.
        /// </summary>
        /// <param name = "model">The model.</param>
        /// <returns>
        ///   A redirect to the account index if the login was successful.
        /// </returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            Contract.Requires<ArgumentNullException>(this.SessionPersister != null, "this.SessionPersister");
            Contract.Requires<ArgumentNullException>(this.Service != null, "this.Service");
            Contract.Requires<ArgumentNullException>(model != null, "model");

            if (this.ModelState.IsValid)
            {
                try
                {
                    var sessionId = this.Service.CreateSession(model.Email, model.Password);
                    this.SessionPersister.SessionId = sessionId;

                    if (string.IsNullOrEmpty(model.ReturnUrl) || !this.Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return this.RedirectToAction("Index", "Account");
                    }

                    return new RedirectResult(model.ReturnUrl);
                }
                catch (FaultException fe)
                {
                    if (fe.Code.Name == this.FaultFactory.UnrecognizedCredentialsFaultCode.Name)
                    {
                        this.ModelState.AddModelError(string.Empty, "The e-mail or password provided is incorrect.");
                    }

                    throw;
                }
            }

            return this.View(model);
        }

        /// <summary>
        ///   Register the user with the specified e-mail and password.
        /// </summary>
        /// <param name = "model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            Contract.Requires<InvalidOperationException>(this.Service != null);
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null);
            Contract.Requires<ArgumentNullException>(model != null, "model");

            if (this.ModelState.IsValid)
            {
                this.Service.CreateUser(model.Email, model.Password);
                this.SessionPersister.SessionId = this.Service.CreateSession(model.Email, model.Password);
                return this.RedirectToAction("Index", "Account");
            }

            return this.View(model);
        }

        public ViewResult Register()
        {
            return this.View(new RegisterViewModel());
        }
    }
}