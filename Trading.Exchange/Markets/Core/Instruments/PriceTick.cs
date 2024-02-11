using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments
{
    internal class PriceTick : IPriceTick
    {
        public PriceTick(decimal price, DateTime dateTime)
        {
            Price = price;
            DateTime = dateTime;
        }

        public decimal Price { get; }

        public DateTime DateTime { get; }
    }
}
