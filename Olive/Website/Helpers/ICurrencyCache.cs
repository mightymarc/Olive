// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICurrencyCache.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the ICurrencyCache type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    ///   Cache of currencies.
    /// </summary>
    public interface ICurrencyCache
    {
        /// <summary>
        ///   Gets the cached currencies.
        /// </summary>
        List<string> Currencies { get; }
    }
}