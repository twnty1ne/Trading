using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Realtime
{
    internal class RealtimeFuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IFuturesInstrument _instrument;

        public RealtimeFuturesInstrument(IInstrumentName name, IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _ = name ?? throw new ArgumentNullException(nameof(name));
            _instrument = new FuturesInstrument(name, _connection.GetInstrumentStream(name), _connection);
            _instrument.OnPriceUpdated += HandlePriceUpdated;
        }

        public IInstrumentName Name { get => _instrument.Name; }

        public event EventHandler<IPriceTick> OnPriceUpdated;

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
