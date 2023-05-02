using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Storage
{
    internal interface IExchangeInfoStorage
    {
        bool TryGetCandles(IInstrumentName name, ConnectionEnum connection, Timeframes timeframe, out IEnumerable<Candle> candles);
        bool TryGetSymbol(IInstrumentName name, ConnectionEnum connection, out IInstrumentInfo info);
    }
}