// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Olive">
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
//   Defines the UserController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;
    using System.Web.Mvc;

    using Olive.Website.ViewModels.User;

    /// <summary>
    /// The user controller.
    /// </summary>
    public class UserController : SiteController
    {
        /// <summary>
        /// The login view (GET).
        /// </summary>
        /// <param name="returnUrl">
        /// The return Url.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult Login(string returnUrl)
        {
            this.ViewData.Add("ReturnUrl", returnUrl);

            return this.View();
        }

        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// A redirect to the account index if the login was successful.
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
                        return this.RedirectToAction(string.Empty, "Account");
                    }

                    return new RedirectResult(model.ReturnUrl);
                }
                catch (FaultException fe)
                {
                    if (fe.Code.Name == this.FaultFactory.UnrecognizedCredentialsFaultCode.Name)
                    {
                        this.ModelState.AddModelError(string.Empty, "The e-mail or password provided is incorrect.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// Register the user with the specified e-mail and password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            Contract.Requires<InvalidOperationException>(this.Service != null);
            Contract.Requires<InvalidOperationException>(this.SessionPersister != null);
            Contract.Requires<ArgumentNullException>(model != null, "model");

            if (this.SessionPersister.HasSession)
            {
                throw new InvalidOperationException("The user is already logged in.");
            }

            if (this.ModelState.IsValid)
            {
                this.Service.CreateUser(model.Email, model.Password);
                this.SessionPersister.SessionId = this.Service.CreateSession(model.Email, model.Password);
                return this.RedirectToAction(string.Empty, "Account");
            }

            return this.View(model);
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult Register()
        {
            if (this.SessionPersister.HasSession)
            {
                throw new InvalidOperationException("The user is already logged in.");
            }

            return this.View(new RegisterViewModel());
        }
    }
}