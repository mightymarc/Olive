using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class TransferViewModel
    {
        public string Description { get; set; }

        public int DestAccountId { get; set; }

        public int SourceAccountId { get; set; }

        public decimal Amount { get; set; }
    }
}