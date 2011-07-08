// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Model used by the Login view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    ///   Model used by the Login view.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}