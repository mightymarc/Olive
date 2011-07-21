using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.ViewModels.Account
{
    public class WithdrawViewModel
    {
        public string BitcoinReceiveAddress { get; set; }

        public int SourceAccountId { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}