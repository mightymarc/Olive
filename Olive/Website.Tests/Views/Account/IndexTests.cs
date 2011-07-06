// -----------------------------------------------------------------------
// <copyright file="IndexTests.cs" company="Microsoft">
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

    using Olive.Services;
    using Olive.Website.ViewModels.Account;
    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    [TestFixture]
    public class IndexTests
    {
        [Test]
        public void RendersWithoutExceptions()
        {
            var view = new Index();

            var viewModel = new IndexViewModel
                {
                    Accounts =
                        new AccountOverview
                            {
                                new AccountOverviewAccount
                                    {
                                        AccountId = 100,
                                        Balance = 100.00m,
                                        CurrencyId = "USD",
                                        DisplayName = "First account"
                                    },
                                new AccountOverviewAccount
                                    {
                                        AccountId = 100,
                                        Balance = 150.06m,
                                        CurrencyId = "UBTC",
                                        DisplayName = "First account"
                                    }
                            }
                };

            var html = view.RenderAsHtml(viewModel);
        }
    }
}
