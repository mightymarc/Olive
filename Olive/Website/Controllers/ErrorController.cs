using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.Controllers
{
    using System.Security.Cryptography;
    using System.Threading;
    using System.Web.Mvc;

    public class ErrorController : SiteController
    {
        public ActionResult NotFound()
        {
            return this.View();
        }

        public ActionResult Error()
        {
            SleepForRandomDuration();

            return this.View();
        }

        private void SleepForRandomDuration()
        {
            var delay = new byte[1];
            using (var prng = new RNGCryptoServiceProvider())
            {
                prng.GetBytes(delay);
                Thread.Sleep(delay[0]);
            }
        }
    }
}