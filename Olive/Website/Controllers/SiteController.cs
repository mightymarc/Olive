// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteController.cs" company="">
//   
// </copyright>
// <summary>
//   The site controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;
    using Olive.Services;

    /// <summary>
    /// The site controller.
    /// </summary>
    public abstract class SiteController : Controller
    {
        [Dependency]
        public ISiteSessionPersister SessionPersister { get; set; }

        [Dependency]
        public IWebService Service { get; set; }

        [Dependency]
        public HttpContextBase Context { get; set; }

        protected ActionResult RedirectToLogin()
        {
            return RedirectToAction(
                "Auth", 
                "Account", 
                new RouteValueDictionary { { "redirectUrl", this.Context.Request.RawUrl } });
        }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
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
    }
}