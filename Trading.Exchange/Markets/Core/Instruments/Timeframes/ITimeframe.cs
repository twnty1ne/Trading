using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments.Candles;

namespace Trading.Exchange.Markets.Core.Instruments.Timeframes
{
    public interface ITimeframe
    {
        event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;
        IInstrumentName InstrumentName { get; }
        Timeframes Type { get; }
        TimeSpan Span { get; }
    }
}
