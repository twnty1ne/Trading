using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Replay;

namespace Trading.Exchange.Markets.HistorySimulation
{
    public class HistorySimulationFuturesUsdtMarket : IMarket<IFuturesInstrument>, IReplayable
    {
        private readonly IConnection _connection;
        private readonly IMarket<IFuturesInstrument> _market;
        private readonly IMarketTicker _ticker;
        private readonly VirtualBalance _balance;

        internal HistorySimulationFuturesUsdtMarket(IConnection connection)
        {
            _ticker = new MarketTicker();
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _balance = new VirtualBalance(500m);
            _market = new FuturesUsdtMarket(x => new HistorySimulationFuturesInstrument(x, _connection, _ticker, _balance));
        }

        public IBalance Balance { get => _balance; }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return _market.GetInstrument(name);
        }

        public IReplay GetReplay(DateTime from, DateTime to)
        {
            return new Replay(_ticker, from, to);
        }
    }
}

