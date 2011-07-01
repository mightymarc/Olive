namespace Olive.Website.Controllers
{
    using System;
    using System.Web.Mvc;

    using Olive.Website.ViewModels.Home;

    public class HomeController : SiteController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
