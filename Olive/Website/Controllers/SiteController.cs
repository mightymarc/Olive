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
    using System.Web.Mvc;

    using Olive.DataAccess;

    /// <summary>
    /// The site controller.
    /// </summary>
    public abstract class SiteController : Controller
    {
        protected ActionResult RedirectToLogin()
        {
            return RedirectToAction("Auth", "Account");
        }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SessionDoesNotExistException)
            {
                SiteSessionPersister.SessionId = Guid.Empty;
                filterContext.ExceptionHandled = true;
                this.Response.Redirect("/Account/Auth");
                return;
            }

            base.OnException(filterContext);
        }
    }
}