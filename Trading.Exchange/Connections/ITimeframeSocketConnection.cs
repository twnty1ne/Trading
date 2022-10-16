using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Candles;

namespace Trading.Exchange.Connections
{
    public interface ITimeframeSocketConnection
    {
        event EventHandler<ICandle> OnCandleClosed;
    }
}
