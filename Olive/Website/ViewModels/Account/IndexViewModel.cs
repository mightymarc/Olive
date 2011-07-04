using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Account
{
    using Olive.Services;

    public class IndexViewModel
    {
        public List<AccountOverview> Accounts { get; set; }
    }
}