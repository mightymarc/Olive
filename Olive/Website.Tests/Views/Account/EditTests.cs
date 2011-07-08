// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the EditTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
    using NUnit.Framework;

    using Olive.Website.ViewModels.Account;
    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    [TestFixture]
    public class EditTests
    {
        [Test]
        public void WithViewModelRendersWithoutExceptions1()
        {
            var view = new Edit();

            var viewModel = new EditViewModel { AccountId = 123, DisplayName = null };

            var html = view.RenderAsHtml(viewModel);
        }

        [Test]
        public void WithViewModelRendersWithoutExceptions2()
        {
            var view = new Edit();

            var viewModel = new EditViewModel
                {
                   AccountId = 123, DisplayName = "QUITEUNIQUE" 
                };

            var html = view.RenderAsHtml(viewModel);
        }

        [Test]
        public void WithoutViewModelRendersWithoutExceptions()
        {
            var view = new Edit();
            var viewModel = new EditViewModel { AccountId = 612345 };

            var html = view.RenderAsHtml(viewModel);
        }
    }
}