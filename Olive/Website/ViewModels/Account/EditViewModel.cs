// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditViewModel.cs" company="Olive">
//   [Copyright]
// </copyright>
// <summary>
//   Defines the EditViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// View model for the Edit view.
    /// </summary>
    public sealed class EditViewModel
    {
        /// <summary>
        /// Gets or sets the id of the account being edited.
        /// </summary>
        /// <value>
        /// The account's id.
        /// </value>
        [HiddenInput(DisplayValue = false)]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [StringLength(150)]
        public string DisplayName { get; set; }
    }
}
