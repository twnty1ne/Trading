using System.Collections.Generic;
using System.Threading.Tasks;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections
{
    public interface IConnection
    {
        Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe);
        IInstrumentStream GetInstrumentStream(IInstrumentName name);
        IInstrumentStream GetHistoryInstrumentStream(IInstrumentName name, IMarketTicker ticker);
    }
}
