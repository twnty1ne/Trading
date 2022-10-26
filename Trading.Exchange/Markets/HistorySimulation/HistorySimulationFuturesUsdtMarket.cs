using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal class HistorySimulationFuturesUsdtMarket : IMarket<IFuturesInstrument>
    {
        private readonly IConnection _connection;
        private readonly IMarket<IFuturesInstrument> _market;

        public HistorySimulationFuturesUsdtMarket(IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _market = new FuturesUsdtMarket(x => new HistorySimulationFuturesInstrument(x, _connection));
        }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return _market.GetInstrument(name);
        }
    }
}
