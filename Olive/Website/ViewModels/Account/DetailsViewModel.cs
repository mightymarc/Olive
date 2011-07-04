using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Account
{
    using Olive.Services;

    public class DetailsViewModel
    {
        public string AccountDisplayName { get; set; }

        public List<GetAccountTransfersTransfer> Transfers { get; set; }
    }
}