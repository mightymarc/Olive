namespace Olive.Website.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CreateAccountViewModel
    {
        /// <summary>
        /// Gets or sets the currency id.
        /// </summary>
        /// <value>
        /// The currency id.
        /// </value>
        [HiddenInput(DisplayValue = false)]
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
    }
}
