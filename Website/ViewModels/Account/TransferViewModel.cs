using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class TransferViewModel
    {
        public string ToComment { get; set; }

        public string FromComment { get; set; }

        public int ToAccountId { get; set; }

        public int FromAccountId { get; set; }

        public decimal Amount { get; set; }
    }
}