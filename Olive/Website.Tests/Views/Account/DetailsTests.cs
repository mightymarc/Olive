// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailsTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the DetailsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
    using NUnit.Framework;

    using Olive.Website.ViewModels.Account;
    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    [TestFixture]
    public class DetailsTests
    {
        [Test]
        public void RendersWithoutExceptions()
        {
            var view = new Details();
            var viewModel = new DetailsViewModel();

            view.RenderAsHtml(viewModel);
        }
    }
}