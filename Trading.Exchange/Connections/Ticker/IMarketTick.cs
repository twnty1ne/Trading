using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Connections.Ticker
{
    public interface IMarketTick
    {
        DateTime Date { get; }
    }
}
