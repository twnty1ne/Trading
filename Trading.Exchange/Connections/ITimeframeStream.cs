using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments.Candles;

namespace Trading.Exchange.Connections
{
    public interface ITimeframeStream
    {
        event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;
        event EventHandler<ICandle> OnCandleOpened;
    }
}
