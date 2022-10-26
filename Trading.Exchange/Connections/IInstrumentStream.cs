using System;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections
{
    public interface IInstrumentStream
    {
        event EventHandler<decimal> OnPriceUpdated;
        ITimeframeStream GetTimeframeStream(Timeframes timeframe);
    }
}
