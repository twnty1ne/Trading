using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Connections
{
    public interface IInstrumentSocketConnection
    {
        event EventHandler<decimal> OnPriceUpdated;
        ITimeframeSocketConnection GetTimeframeSocketConnection(Timeframes timeframe);
    }
}
