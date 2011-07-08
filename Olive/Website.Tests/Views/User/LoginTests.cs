// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the LoginTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.User
{
    using NUnit.Framework;

    using Olive.Website.ViewModels.User;
    using Olive.Website.Views.User;

    using PrecompiledMvcViews.Testing;

    [TestFixture]
    public class LoginTests
    {
        [Test]
        public void RendersWithoutExceptions()
        {
            var view = new Login();

            var html = view.RenderAsHtml();
        }

        [Test]
        public void WithViewModelRendersWithoutExceptions()
        {
            var view = new Login();

            var viewModel = new LoginViewModel { Password = "123", Email = "myemail" };

            var html = view.RenderAsHtml(viewModel);
        }
    }
}