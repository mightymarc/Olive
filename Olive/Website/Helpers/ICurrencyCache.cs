// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICurrencyCache.cs" company="Olive">
//   [Copyright]
// </copyright>
// <summary>
//   Defines the ICurrencyCache type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Cache of currencies.
    /// </summary>
    public interface ICurrencyCache
    {
        /// <summary>
        /// Gets the cached currencies.
        /// </summary>
        List<string> Currencies { get; }
    }
}
