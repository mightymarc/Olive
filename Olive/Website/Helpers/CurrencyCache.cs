// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrencyCache.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the CurrencyCache type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Helpers
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Microsoft.Practices.Unity;

    using Olive.Services;

    public class CurrencyCache : ICurrencyCache
    {
        public List<string> Currencies
        {
            get
            {
                Contract.Requires(this.Service != null, "this.Service != null");

                return this.Service.GetCurrencies();
            }
        }

        [Dependency]
        public IWebService Service { get; set; }
    }
}