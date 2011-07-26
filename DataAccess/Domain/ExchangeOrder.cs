// -----------------------------------------------------------------------
// <copyright file="ExchangeOrder.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ExchangeOrder
    {
        public int OrderId { get; set; }

        public int SourceAccountId { get; set; }

        public virtual Account SourceAccount { get; set; }

        public int DestAccountId { get; set; }

        public virtual Account DestAccount { get; set; }

        public decimal Volume { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
