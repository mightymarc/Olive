// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="">
//   
// </copyright>
// <summary>
//   The currency.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The currency.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Gets or sets the currency identifier (USD, BTC, PPUSD, ...).
        /// </summary>
        public string CurrencyId { get; set; }
    }
}