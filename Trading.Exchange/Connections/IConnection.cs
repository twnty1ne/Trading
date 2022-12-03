using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Connections
{
    public interface IConnection
    {
        Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, TimeframeEnum timeframe);
    }
}
