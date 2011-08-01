// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveSiteInterfaceTests.cs" company="Olive">
//   Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    
//    1. Definitions
//    
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    
//    A "contribution" is the original software, or any additions or changes to the software.
//    
//    A "contributor" is any person that distributes its contribution under this license.
//    
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    
//    2. Grant of Rights
//    
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    
//    3. Conditions and Limitations
//    
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// </copyright>
// <summary>
//   Defines the OliveSiteInterfaceTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Tools.UpstreamTrader.Tests.Sites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Moq;

    using NUnit.Framework;

    using Olive.Services;
    using Olive.UpstreamTrader;
    using Olive.UpstreamTrader.Sites;

    [TestFixture]
    public class OliveSiteInterfaceTests
    {
        [Test]
        public void LoginSuccessTest()
        {
            var email = UnitTestHelper.GetRandomEmail();
            var password = UnitTestHelper.GetRandomDisplayName();
            var sessionId = Guid.NewGuid();

            var service = new Mock<IClientService>();
            service.Setup(s => s.CreateSession(email, password)).Returns(sessionId);

            var config = new Mock<SiteConfiguration>();

            var site = new OliveSiteInterface(config.Object) { Email = email, Password = password, ClientService = service.Object };
            site.Login();

            service.Verify(s => s.CreateSession(email, password), Times.Once());
        }

        [Test]
        public void GetMarketPricesTest()
        {
            // Arrange
            var response = new List<GetMarketPricesResponse>
                {
                    new GetMarketPricesResponse
                        {
                            FromCurrency = "BTC",
                            ToCurrency = "USD",
                            MarketId = 100,
                            Prices =
                                new List<GetMarketPricesResponsePrice>
                                    {
                                        new GetMarketPricesResponsePrice { Price = 100.5m, Volume = 50m },
                                        new GetMarketPricesResponsePrice { Price = 50.5m, Volume = 20.59m }
                                    }
                        },
                    new GetMarketPricesResponse
                        {
                            FromCurrency = "USD",
                            ToCurrency = "BTC",
                            MarketId = 101,
                            Prices =
                                new List<GetMarketPricesResponsePrice>
                                    {
                                        new GetMarketPricesResponsePrice { Price = 10.5m, Volume = 50m },
                                        new GetMarketPricesResponsePrice { Price = 5.5m, Volume = 200.59m }
                                    }
                        }
                };

            var service = new Mock<IClientService>();
            service.Setup(s => s.GetAllMarketPrices()).Returns(response);

            var config = new Mock<SiteConfiguration>();
            var site = new Mock<OliveSiteInterface>(config.Object) { CallBase = true };
            site.SetupGet(s => s.ClientService).Returns(service.Object);

            // Act
            var result = site.Object.GetMarketPrices();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2 * 2, result.Count);
            Assert.AreEqual(response[0].MarketId.ToString(), result[0].MarketKey);
            Assert.AreEqual(response[0].FromCurrency, result[0].FromCurrency);
            Assert.AreEqual(response[0].ToCurrency, result[0].ToCurrency);
            Assert.AreEqual(response[0].Prices[0].Price, result[0].Price);
            Assert.AreEqual(response[0].Prices[0].Volume, result[0].Volume);
            Assert.AreEqual(response[0].MarketId.ToString(), result[1].MarketKey);
            Assert.AreEqual(response[0].FromCurrency, result[1].FromCurrency);
            Assert.AreEqual(response[0].ToCurrency, result[1].ToCurrency);
            Assert.AreEqual(response[0].Prices[1].Price, result[1].Price);
            Assert.AreEqual(response[0].Prices[1].Volume, result[1].Volume);
            Assert.AreEqual(response[1].MarketId.ToString(), result[2].MarketKey);
            Assert.AreEqual(response[1].FromCurrency, result[2].FromCurrency);
            Assert.AreEqual(response[1].ToCurrency, result[2].ToCurrency);
            Assert.AreEqual(response[1].Prices[0].Price, result[2].Price);
            Assert.AreEqual(response[1].Prices[0].Volume, result[2].Volume);
            Assert.AreEqual(response[1].MarketId.ToString(), result[3].MarketKey);
            Assert.AreEqual(response[1].FromCurrency, result[3].FromCurrency);
            Assert.AreEqual(response[1].ToCurrency, result[3].ToCurrency);
            Assert.AreEqual(response[1].Prices[1].Price, result[3].Price);
            Assert.AreEqual(response[1].Prices[1].Volume, result[3].Volume);

            service.Verify(s => s.GetAllMarketPrices());
        }

        [Test]
        public void GetMarketPricesForMarketSuccessTest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GetMarketPricesForMarket_MarketDoesNotExist_ThrowsExceptionTest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GetMarketsTest()
        {
            var response = new List<GetMarketResponse>
                {
                    new GetMarketResponse
                        {
                            FromCurrencyId = "BTC",
                            ToCurrencyId = "USD",
                            MarketId = 1
                        },
                    new GetMarketResponse
                        {
                            FromCurrencyId = "USD",
                            ToCurrencyId = "BTC",
                            MarketId = 2
                        }
                };

            var service = new Mock<IClientService>();
            service.Setup(s => s.GetMarkets()).Returns(response);

            var config = new Mock<SiteConfiguration>();
            var site = new Mock<OliveSiteInterface>(config.Object) { CallBase = true };
            site.SetupGet(s => s.ClientService).Returns(service.Object);
            site.SetupGet(s => s.LoggedIn).Returns(true);

            var markets = site.Object.GetMarkets();
            var marketsTyped = markets;
            Assert.AreEqual(2, markets.Count);
            Assert.AreEqual(response[0].FromCurrencyId, marketsTyped[0].FromCurrency);
            Assert.AreEqual(response[0].ToCurrencyId, marketsTyped[0].ToCurrency);
            Assert.AreEqual(response[0].MarketId, marketsTyped[0].MarketKey);
            Assert.AreEqual(response[1].FromCurrencyId, marketsTyped[1].FromCurrency);
            Assert.AreEqual(response[1].ToCurrencyId, marketsTyped[1].ToCurrency);
            Assert.AreEqual(response[1].MarketId, marketsTyped[1].MarketKey);

            site.Verify(s => s.LoggedIn, Times.Once());
            service.Verify(s => s.GetMarkets(), Times.Once());
        }
    }
}
