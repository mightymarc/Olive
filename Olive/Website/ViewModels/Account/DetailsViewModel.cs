// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailsViewModel.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the DetailsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.ViewModels.Account
{
    using System.Collections.Generic;

    using Olive.Services;

    public class DetailsViewModel
    {
        public string AccountDisplayName { get; set; }

        public List<GetAccountTransfersTransfer> Transfers { get; set; }
    }
}