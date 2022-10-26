using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Markets.HistorySimulation
{
    internal class HistorySimulationFuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IFuturesInstrument _instrument;

        public HistorySimulationFuturesInstrument(IInstrumentName name, IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _instrument = new FuturesInstrument(name, _connection.GetHistoryInstrumentStream(name), _connection);
        }

        public IInstrumentName Name { get => _instrument.Name; }

        public ITimeframe GetTimeframe(Timeframes type)
        {
            return _instrument.GetTimeframe(type);
        }
    }
}
