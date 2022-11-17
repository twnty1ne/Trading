using System;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections
{
    public interface IInstrumentStream
    {
        event EventHandler<IPriceTick> OnPriceUpdated;
        ITimeframeStream GetTimeframeStream(Timeframes timeframe);
    }
}
