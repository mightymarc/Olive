// -----------------------------------------------------------------------
// <copyright file="ExchangeControllerTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.ViewModels.Exchange;

    [TestFixture]
    public class ExchangeControllerTests : ControllerTestBase<ExchangeController>
    {
        [Test]
        public void IndexTest1()
        {
            // Arrange
            var controller = this.CreateController();

            this.currencyCache.SetupGet(c => c.Currencies).Returns(new List<string>(new[] { "EXU", "USD" }));

            var marketResponses = new List<GetMarketResponse>
                {
                    new GetMarketResponse
                        {
                            FromCurrencyId = "EXU",
                            ToCurrencyId = "USD",
                            MarketId = 101
                        },
                    new GetMarketResponse
                        {
                            FromCurrencyId = "USD",
                            ToCurrencyId = "EXU",
                            MarketId = 102
                        }
                };

            var marketPriceResponses = new List<GetMarketPricesResponse>
                {
                    new GetMarketPricesResponse
                        {
                            MarketId = 101,
                            Prices = new List<GetMarketPricesResponsePrice>
                                {
                                    new GetMarketPricesResponsePrice
                                        {
                                            Price = 10,
                                            Volume = 150
                                        },
                                    new GetMarketPricesResponsePrice
                                        {
                                            Price = 12,
                                            Volume = 120
                                        },
                                    new GetMarketPricesResponsePrice
                                        {
                                            Price = 13,
                                            Volume = 120
                                        }
                                }
                        },
                    new GetMarketPricesResponse
                        {
                            MarketId = 102,
                            Prices = new List<GetMarketPricesResponsePrice>
                                {
                                    new GetMarketPricesResponsePrice
                                        {
                                            Price = 1m / 14,
                                            Volume = 150
                                        },
                                    new GetMarketPricesResponsePrice
                                        {
                                            Price = 1m / 15,
                                            Volume = 120
                                        }
                                }
                        }
                };

            this.serviceMock.Setup(c => c.GetMarket("EXU", "USD")).Returns(marketResponses[0]);
            this.serviceMock.Setup(c => c.GetMarket("USD", "EXU")).Returns(marketResponses[1]);
            this.serviceMock.Setup(c => c.GetMarketPrices(101)).Returns(marketPriceResponses[0]);
            this.serviceMock.Setup(c => c.GetMarketPrices(102)).Returns(marketPriceResponses[1]);

            // Act
            var actionResult = controller.Index();

            // Assert
            var viewResult = (ViewResult)actionResult;
            var viewModel = (IndexViewModel)viewResult.Model;

            Assert.IsNotNull(viewModel.Markets);
            Assert.AreEqual(2, viewModel.Markets.Count);
            Assert.IsNotNull(viewModel.Markets[0].Item1);
            Assert.IsNotNull(viewModel.Markets[0].Item2);
            Assert.AreEqual("EXU", viewModel.Markets[0].Item1.FromCurrencyId);
            Assert.AreEqual("USD", viewModel.Markets[0].Item1.ToCurrencyId);
            Assert.AreEqual(101, viewModel.Markets[0].Item1.MarketId);
            Assert.AreEqual(101, viewModel.Markets[0].Item2.MarketId);
            Assert.AreEqual(3, viewModel.Markets[0].Item2.Prices.Count);
            Assert.AreEqual(10, viewModel.Markets[0].Item2.Prices[0].Price);
            Assert.AreEqual(150, viewModel.Markets[0].Item2.Prices[0].Volume);
            Assert.AreEqual(12, viewModel.Markets[0].Item2.Prices[1].Price);
            Assert.AreEqual(120, viewModel.Markets[0].Item2.Prices[1].Volume);
            Assert.AreEqual(13, viewModel.Markets[0].Item2.Prices[2].Price);
            Assert.AreEqual(120, viewModel.Markets[0].Item2.Prices[2].Volume);

            Assert.IsNotNull(viewModel.Markets[1].Item1);
            Assert.IsNotNull(viewModel.Markets[1].Item2);
            Assert.AreEqual("USD", viewModel.Markets[1].Item1.FromCurrencyId);
            Assert.AreEqual("EXU", viewModel.Markets[1].Item1.ToCurrencyId);
            Assert.AreEqual(102, viewModel.Markets[1].Item1.MarketId);
            Assert.AreEqual(102, viewModel.Markets[1].Item2.MarketId);
            Assert.AreEqual(2, viewModel.Markets[1].Item2.Prices.Count);
            Assert.AreEqual(1m / 14, viewModel.Markets[1].Item2.Prices[0].Price);
            Assert.AreEqual(150, viewModel.Markets[1].Item2.Prices[0].Volume);
            Assert.AreEqual(1m / 15, viewModel.Markets[1].Item2.Prices[1].Price);
            Assert.AreEqual(120, viewModel.Markets[1].Item2.Prices[1].Volume);

            this.serviceMock.Verify(c => c.GetMarket("EXU", "USD"), Times.Once());
            this.serviceMock.Verify(c => c.GetMarket("USD", "EXU"), Times.Once());
            this.serviceMock.Verify(c => c.GetMarketPrices(101), Times.Once());
            this.serviceMock.Verify(c => c.GetMarketPrices(102), Times.Once());
        }

        [Test]
        public void CreateOrderGetTest()
        {
            var controller = this.CreateController();
            var marketId = UnitTestHelper.Random.Next(1, 1000000);
            var price = UnitTestHelper.Random.Next(1, 10000000) / 1000m;
            var sessionId = this.SetupHasSession();

            var actionResult = controller.CreateOrder(marketId, price);

            var viewResult = this.AssertViewResult(actionResult);
            var viewModel = this.AssertViewModel<CreateOrderViewModel>(actionResult);

            Assert.AreEqual(marketId, viewModel.MarketId);
            Assert.AreEqual(price, viewModel.Price);
        }

        [Test]
        public void CreateOrderPostTest()
        {
            var controller = this.CreateController();
            var marketId = UnitTestHelper.Random.Next(1, 1000000);
            var price = UnitTestHelper.Random.Next(1, 10000000) / 1000m;
            var sessionId = this.SetupHasSession();
            var orderId = UnitTestHelper.Random.Next(1, int.MaxValue);

            var viewModel = new CreateOrderViewModel
                {
                    MarketId = marketId,
                    Price = price
                };

            this.serviceMock.Setup(s => s.CreateOrder(sessionId, 0, 0, marketId, price, 0)).Returns(orderId);

            var actionResult = (RedirectToRouteResult)controller.CreateOrder(viewModel);

            this.serviceMock.Verify(s => s.CreateOrder(sessionId, 0, 0, marketId, price, 0), Times.Once());
        }
    }
}
