using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Replay;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Markets.HistorySimulation
{
    public class HistorySimulationFuturesUsdtMarket : IMarket<IFuturesInstrument>, IReplayable
    {
        private readonly IMarket<IFuturesInstrument> _market;
        private readonly IMarketTicker _ticker;
        private readonly VirtualBalance _balance;

        internal HistorySimulationFuturesUsdtMarket(IConnection connection, IRange<DateTime> range)
        {
            _ticker = new MarketTicker(range);
            _balance = new VirtualBalance(500m);
            _market = new FuturesUsdtMarket(x => 
                new HistorySimulationFuturesInstrument(x, connection, _ticker, _balance));
        }

        public IBalance Balance { get => _balance; }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return _market.GetInstrument(name);
        }

        public IReplay GetReplay()
        {
            return new Replay(_ticker);
        }
    }
}

