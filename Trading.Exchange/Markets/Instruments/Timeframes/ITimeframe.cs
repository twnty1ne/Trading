using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Candles;

namespace Trading.Exchange.Markets.Instruments.Timeframes
{
    public interface ITimeframe
    {
        event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;

        IInstrumentName InstrumentName { get; }
        Timeframes Type { get; }
        TimeSpan Span { get; }
        IReadOnlyCollection<ICandle> GetCandles();
    }
}
