using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Connections.Chunks
{
    public interface ICandlesChunk
    {
        event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;                                                           
        event EventHandler<ICandle> OnCandleOpened;
        IRange<DateTime> Range { get; }
        void Load();
    }
}