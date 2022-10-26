using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Markets
{
    public interface IMarket
    {
        IMarket<IFuturesInstrument> RealtimeFuturesUsdt { get; }
        IMarket<IFuturesInstrument> HistorySimulationFuturesUsdt { get; }
    }
}
