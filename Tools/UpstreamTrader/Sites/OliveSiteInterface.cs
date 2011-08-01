// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveSiteInterface.cs" company="Olive">
//   Microsoft Public License (Ms-PL)
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    1. Definitions
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    A "contribution" is the original software, or any additions or changes to the software.
//    A "contributor" is any person that distributes its contribution under this license.
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    2. Grant of Rights
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    3. Conditions and Limitations
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// </copyright>
// <summary>
//   Defines the OliveSiteInterface type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.UpstreamTrader.Sites
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Microsoft.Practices.Unity;

    using Olive.Services;

    /// <summary>
    ///   TODO: Update summary.
    /// </summary>
    public class OliveSiteInterface : SiteInterface
    {
        private Guid sessionId;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "OliveSiteInterface" /> class.
        /// </summary>
        /// <param name = "siteConfiguration">The site configuration.</param>
        public OliveSiteInterface(SiteConfiguration siteConfiguration)
            : base(siteConfiguration)
        {
        }

        [Dependency]
        public virtual IClientService ClientService { get; set; }

        public string Email { get; set; }

        public virtual bool LoggedIn
        {
            get
            {
                return this.sessionId != Guid.Empty;
            }
        }

        public string Password { get; set; }

        public override List<Market> GetMarkets()
        {
            if (!this.LoggedIn)
            {
                throw new InvalidOperationException("Not logged in.");
            }

            return (from m in this.ClientService.GetMarkets()
                    select
                        new Market
                            {
                               FromCurrency = m.FromCurrencyId, ToCurrency = m.ToCurrencyId, MarketKey = m.MarketId.ToString()
                            }).ToList();
        }

        public override List<MarketPrice> GetPrices(string marketKey)
        {
            throw new NotImplementedException();
        }

        public override List<Price> GetPrices()
        {
            throw new NotImplementedException();
        }

        public virtual void Login()
        {
            this.sessionId = this.ClientService.CreateSession(this.Email, this.Password);
        }

        public List<Price> GetMarketPrices()
        {
            var marketPrices = this.ClientService.GetAllMarketPrices();

            return (from marketPrice in marketPrices
                    from price in marketPrice.Prices
                    select
                        new Price
                            {
                                FromCurrency = marketPrice.FromCurrency,
                                MarketKey = marketPrice.MarketId.ToString(CultureInfo.InvariantCulture),
                                ToCurrency = marketPrice.ToCurrency,
                                Price = price.Price,
                                Volume = price.Volume
                            }).ToList();
        }
    }
}