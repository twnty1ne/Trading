using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Storage;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Shared.Excel
{
    internal interface IExchangeInfoStorage
    {
        bool TryGetCandles(IInstrumentName name, ConnectionEnum connection, Timeframes timeframe, out IEnumerable<Candle> candles);
        bool TryGetSymbol(IInstrumentName name, ConnectionEnum connection, out IInstrumentInfo info);
    }
}