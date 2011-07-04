using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Account
{
    public class AuthViewModel
    {
        public bool LoginError { get; set; }

        public string RegisterEmail { get; set; }

        public string RegisterPassword { get; set; }

        public string LoginEmail { get; set; }

        public string LoginPassword { get; set; }
    }
}