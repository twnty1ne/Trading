using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Connections.Ticker
{
    public interface IMarketTicker
    {
        event EventHandler<IMarketTick> OnTick;
        event EventHandler OnStopped;
        void Start(DateTime from);
        void Reset();
    }
}
