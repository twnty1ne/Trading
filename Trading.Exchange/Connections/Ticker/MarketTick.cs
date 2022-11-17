using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Connections.Ticker
{
    internal class MarketTick : IMarketTick
    {
        public MarketTick(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }
    }
}
