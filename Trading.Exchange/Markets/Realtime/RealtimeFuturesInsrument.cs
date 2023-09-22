using System;
using System.Collections.Generic;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
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
            _instrument.OnPositionOpened += HandlePositionOpened;
        }

        public IInstrumentName Name { get => _instrument.Name; }
        public decimal Price { get => _instrument.Price; }

        public event EventHandler<IPriceTick> OnPriceUpdated;
        public event EventHandler<IPosition> OnPositionOpened;

        public void SetPositionEntry(PositionSides side,
            int leverage,
            decimal stopLoss,
            IEnumerable<(decimal Price, decimal Volume)> takeProfits,
            decimal size,
            Guid id)
        {
            throw new NotImplementedException();
        }

        public ITimeframe GetTimeframe(Timeframes type)
        {
            return _instrument.GetTimeframe(type);
        }
        
        private void HandlePriceUpdated(object sender, IPriceTick tick)
        {
            OnPriceUpdated?.Invoke(sender, tick);
        }

        private void HandlePositionOpened(object sender, IPosition position)
        {
            OnPositionOpened?.Invoke(sender, position);
        }
    }
}
