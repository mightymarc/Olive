// -----------------------------------------------------------------------
// <copyright file="Details.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    [TestFixture]
    public class DetailsTests
    {
        [Test]
        public void RendersWithoutExceptions()
        {
            var view = new Details();
            var viewModel = new Olive.Website.ViewModels.Account.DetailsViewModel();

            view.RenderAsHtml(viewModel);
        }
    }
}
