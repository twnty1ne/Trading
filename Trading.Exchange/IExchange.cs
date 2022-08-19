using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Exchange
{
    public interface IExchange
    {
        public IMarket Market { get; }
    }
}
