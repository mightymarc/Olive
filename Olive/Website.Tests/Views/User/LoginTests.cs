// -----------------------------------------------------------------------
// <copyright file="Register.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests.Views.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

            var viewModel = new LoginViewModel
            {
                Password = "123",
                Email = "myemail"
            };

            var html = view.RenderAsHtml(viewModel);
        }
    }
}
