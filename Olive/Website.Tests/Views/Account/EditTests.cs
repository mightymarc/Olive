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
                   AccountId = 123, DisplayName = "Quite Unique" 
                };

            var html = view.RenderAsHtml(viewModel);
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//form"), "Form missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='text' and @name='DisplayName']"), "DisplayName textbox missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='submit']"), "Submit button missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//a[@href='/Account']"), "Cancel link missing");
        }

        [Test]
        public void WithoutViewModelRendersWithoutExceptions()
        {
            var view = new Edit();
            var viewModel = new EditViewModel { AccountId = 612345 };

            var html = view.RenderAsHtml(viewModel);

            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//form"), "Form missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='text' and @name='DisplayName' and @value='']"), "DisplayName textbox missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='submit']"), "Submit button missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//a[@href='/Account']"), "Cancel link missing");
        }
    }
}