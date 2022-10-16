using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Exchange.Markets
{
    internal class Market : IMarket
    {
        public IMarket<IFuturesInstrument> FuturesUsdt { get; private set; }

        public Market(IConnection connection)
        {
            FuturesUsdt = new FuturesUsdtMarket(connection);
        }
        
    }
}
