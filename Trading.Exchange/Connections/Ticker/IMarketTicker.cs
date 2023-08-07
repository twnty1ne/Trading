using System;
using System.Collections.Generic;
using System.Text;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Connections.Ticker
{
    public interface IMarketTicker
    {
        IRange<DateTime> TicksRange { get; }
        event EventHandler<IMarketTick> OnTick;
        event EventHandler OnStopped;
        void Start();
        void Reset();
    }
}
