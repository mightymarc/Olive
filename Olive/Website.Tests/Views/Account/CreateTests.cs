// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the CreateTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Olive.Website.ViewModels.Account;
    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void WithViewModelRendersWithoutExceptions1()
        {
            var view = new Create();

            var viewModel = new CreateViewModel
                {
                   Currencies = new List<string> { "USD", "BTC", "MBUSD" }, CurrencyId = "BTC", DisplayName = null 
                };

            var html = view.RenderAsHtml(viewModel);
        }

        [Test]
        public void WithViewModelRendersWithoutExceptions2()
        {
            var view = new Create();

            var viewModel = new CreateViewModel
                {
                    Currencies = new List<string> { "USD", "BTC", "MBUSD" }, 
                    CurrencyId = "BTC", 
                    DisplayName = "QUITEUNIQUE"
                };

            var html = view.RenderAsHtml(viewModel);
        }

        [Test]
        public void WithoutViewModelRendersWithoutExceptions()
        {
            var view = new Create();

            var html = view.RenderAsHtml();
        }
    }
}