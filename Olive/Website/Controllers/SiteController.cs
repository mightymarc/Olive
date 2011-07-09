// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteController.cs" company="Olive">
//   
// </copyright>
// <summary>
//   The site controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;
    using Olive.Services;
    using Olive.Website.Helpers;

    /// <summary>
    ///   The site controller.
    /// </summary>
    public abstract class SiteController : Controller
    {
        [Dependency]
        public ICurrencyCache CurrencyCache { get; set; }

        [Dependency]
        public IWebService Service { get; set; }

        [Dependency]
        public ISiteSession SessionPersister { get; set; }

        [Dependency]
        public IFaultFactory FaultFactory { get; set; }

        /// <summary>
        ///   Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name = "filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SessionDoesNotExistException)
            {
                this.SessionPersister.SessionId = Guid.Empty;
                filterContext.ExceptionHandled = true;
                this.Response.Redirect("/Account/Auth");
                return;
            }

            base.OnException(filterContext);
        }

        protected ActionResult RedirectToLogin()
        {
            Contract.Requires(this.Request != null, "this.Context.Request");

            return this.RedirectToAction(
                "Login", "User", new RouteValueDictionary { { "returnUrl", this.Request.Url.PathAndQuery } });
        }
    }
}