using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.HistorySimulation;
using Trading.Exchange.Markets.Realtime;

namespace Trading.Exchange.Markets
{
    internal class Market : IMarket
    {
        public IMarket<IFuturesInstrument> RealtimeFuturesUsdt { get; }

        public HistorySimulationFuturesUsdtMarket HistorySimulationFuturesUsdt { get; }

        public Market(IConnection connection)
        {
            HistorySimulationFuturesUsdt = new HistorySimulationFuturesUsdtMarket(connection);
            RealtimeFuturesUsdt = new RealtimeFuturesUsdtMarket(connection);
        }
    }
}
