// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the IndexTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
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
                                        AccountId = 102, 
                                        Balance = 150.06m, 
                                        CurrencyId = "UBTC", 
                                        DisplayName = "First account"
                                    }
                            }
                };

            var html = view.RenderAsHtml(viewModel);

            Assert.IsNotNull(html.DocumentNode.SelectSingleNode(".//a[@href='/Account/Create']"), "Create current account link missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode(".//a[@href='/Account/Edit/102']"), "Edit account link missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode(".//a[@href='/Account/Transfer/?SourceAccountId=100']"), "Transfer from account link missing.");
        }
    }
}