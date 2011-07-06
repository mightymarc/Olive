// -----------------------------------------------------------------------
// <copyright file="CreateTests.cs" company="Microsoft">
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
    public class EditTests
    {
        [Test]
        public void WithoutViewModelRendersWithoutExceptions()
        {
            var view = new Edit();

            var html = view.RenderAsHtml();
        }

        [Test]
        public void WithViewModelRendersWithoutExceptions1()
        {
            var view = new Edit();

            var viewModel = new Olive.Website.ViewModels.Account.EditViewModel
                {
                    AccountId = 123,
                    DisplayName = null
                };

            var html = view.RenderAsHtml(viewModel);
        }

        [Test]
        public void WithViewModelRendersWithoutExceptions2()
        {
            var view = new Edit();

            var viewModel = new Olive.Website.ViewModels.Account.EditViewModel
            {
                AccountId = 123,
                DisplayName = "QUITEUNIQUE"
            };

            var html = view.RenderAsHtml(viewModel);
        }
    }
}
