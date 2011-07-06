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
    public class RegisterTests
    {
        [Test]
        public void RendersWithoutExceptions()
        {
            var view = new Register();

            var html = view.RenderAsHtml();
        }

        [Test]
        public void WithViewModelRendersWithoutExceptions()
        {
            var view = new Register();

            var viewModel = new RegisterViewModel
                {
                    ConfirmPassword = "abc",
                    Password = "123",
                    Email = "incorrect"
                };

            var html = view.RenderAsHtml(viewModel);
        }
    }
}
