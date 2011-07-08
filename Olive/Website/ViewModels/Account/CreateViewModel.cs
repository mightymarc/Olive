// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateViewModel.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the CreateViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.ViewModels.Account
{
    using System.Collections.Generic;

    public class CreateViewModel
    {
        public List<string> Currencies { get; set; }

        /// <summary>
        ///   Gets or sets the currency.
        /// </summary>
        /// <value>
        ///   The currency.
        /// </value>
        public string CurrencyId { get; set; }

        /// <summary>
        ///   Gets or sets the display name.
        /// </summary>
        /// <value>
        ///   The display name.
        /// </value>
        public string DisplayName { get; set; }
    }
}