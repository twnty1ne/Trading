using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.HistorySimulation;

namespace Trading.Exchange.Markets
{
    public interface IMarket
    {
        IMarket<IFuturesInstrument> RealtimeFuturesUsdt { get; }
        HistorySimulationFuturesUsdtMarket HistorySimulationFuturesUsdt { get; }
    }
}
