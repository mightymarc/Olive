using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Olive.Website.Controllers
{
    using Olive.Services;
    using Olive.Website.ViewModels.Exchange;

    public class ExchangeController : SiteController
    {
        public ActionResult Index()
        {
            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var viewModel = new IndexViewModel
                {
                    Markets =
                        new List<Tuple<GetMarketResponse, GetMarketPricesResponse>>(
                        from c1 in this.CurrencyCache.Currencies
                        from c2 in this.CurrencyCache.Currencies
                        where c1 != c2
                        let market = this.ClientService.GetMarket(c1, c2)
                        select Tuple.Create(market, this.ClientService.GetMarketPrices(market.MarketId)))
                };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateOrder(int? marketId, decimal? price)
        {
            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            var viewModel = new CreateOrderViewModel
                { Price = price };

            if (marketId.HasValue)
            {
                var market = this.ClientService.GetMarket(marketId.Value);

                var sourceAccount =
                    this.ClientService.GetAccounts(this.SessionPersister.SessionId).FirstOrDefault(
                        x => x.CurrencyId == market.FromCurrencyId);

                if (sourceAccount != null)
                {
                    viewModel.SourceAccountId = sourceAccount.AccountId;
                }

                var destAccount =
                    this.ClientService.GetAccounts(this.SessionPersister.SessionId).FirstOrDefault(
                        x => x.CurrencyId == market.ToCurrencyId);

                if (destAccount != null)
                {
                    viewModel.DestAccountId = destAccount.AccountId;
                }
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrder(CreateOrderViewModel viewModel)
        {
            if (!this.SessionPersister.HasSession)
            {
                return this.RedirectToLogin();
            }

            if (this.ModelState.IsValid)
            {

                var orderId = this.ClientService.CreateOrder(
                    this.SessionPersister.SessionId,
                    viewModel.SourceAccountId.Value,
                    viewModel.DestAccountId.Value,
                    viewModel.Price.Value,
                    viewModel.Volume.Value);

                return this.RedirectToAction(string.Empty);
            }

            return this.View(viewModel);
        }
    }
}
