﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteController.cs" company="Olive">
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
//   The site controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Olive.Services;
    using Olive.Website.Helpers;

    /// <summary>
    /// The site controller.
    /// </summary>
    public abstract class SiteController : Controller
    {
        /// <summary>
        /// Gets or sets CurrencyCache.
        /// </summary>
        /// <value>
        /// The currency cache.
        /// </value>
        [Dependency]
        public ICurrencyCache CurrencyCache { get; set; }

        /// <summary>
        /// Gets or sets ClientService.
        /// </summary>
        /// <value>
        /// The client service.
        /// </value>
        [Dependency]
        public IClientService ClientService { get; set; }

        /// <summary>
        /// Gets or sets SessionPersister.
        /// </summary>
        /// <value>
        /// The session persister.
        /// </value>
        [Dependency]
        public ISiteSession SessionPersister { get; set; }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            var faultException = filterContext.Exception as FaultException;

            if (faultException != null)
            {
                if (faultException.Code.Name == FaultFactory.SessionDoesNotExistFaultCode.Name)
                {
                    this.SessionPersister.RemoveSession();
                    filterContext.ExceptionHandled = true;
                    this.Response.Redirect("/User/Login");
                    return;
                }
            }

            base.OnException(filterContext);
        }

        /// <summary>
        /// The redirect to login.
        /// </summary>
        /// <returns></returns>
        protected ActionResult RedirectToLogin()
        {
            Contract.Requires(this.Request != null, "this.Context.Request");

            return this.RedirectToAction(
                "Login", "User", new RouteValueDictionary { { "returnUrl", this.Request.Url.PathAndQuery } });
        }
    }
}