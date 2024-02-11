using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.HistorySimulation;
using Trading.Exchange.Markets.Realtime;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Markets
{
    internal class Market : IMarket
    {
        public IMarket<IFuturesInstrument> RealtimeFuturesUsdt { get; }

        public HistorySimulationFuturesUsdtMarket HistorySimulationFuturesUsdt { get; }

        public Market(IConnection connection, IRange<DateTime> historyRange, TimeSpan realtimeBuffer)
        {
            HistorySimulationFuturesUsdt = new HistorySimulationFuturesUsdtMarket(connection, historyRange);
            RealtimeFuturesUsdt = new RealtimeFuturesUsdtMarket(connection, realtimeBuffer);
        }
    }
}
