using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal class HistorySimulationFuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IFuturesInstrument _instrument;

        public HistorySimulationFuturesInstrument(IInstrumentName name, IConnection connection, IMarketTicker ticker)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _instrument = new FuturesInstrument(name, _connection.GetHistoryInstrumentStream(name, ticker), _connection);
            _instrument.OnPriceUpdated += HandlePriceUpdated;
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;

        public IInstrumentName Name { get => _instrument.Name; }

        public ITimeframe GetTimeframe(Timeframes type)
        {
            return _instrument.GetTimeframe(type);
        }

        private void HandlePriceUpdated(object sender, IPriceTick tick) 
        {
            OnPriceUpdated?.Invoke(sender, tick);
        }
    }
}
