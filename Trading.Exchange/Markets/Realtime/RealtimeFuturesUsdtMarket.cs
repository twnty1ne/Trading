using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Markets.Realtime
{
    internal class RealtimeFuturesUsdtMarket : IMarket<IFuturesInstrument>
    {
        private readonly IConnection _connection;
        private readonly IMarket<IFuturesInstrument> _market;

        public RealtimeFuturesUsdtMarket(IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _market = new FuturesUsdtMarket(x => new RealtimeFuturesInstrument(x, _connection));
        }

        public IMarketTicker Ticker { get; }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return _market.GetInstrument(name);
        }
    }
}
