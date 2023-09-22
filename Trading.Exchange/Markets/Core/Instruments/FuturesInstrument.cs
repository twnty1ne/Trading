using System;
using System.Collections.Generic;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Resolvers;

namespace Trading.Exchange.Markets.Core.Instruments
{
    internal class FuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IInstrumentStream _stream;
        private readonly IResolver<Timeframes.Timeframes, ITimeframe> _resolver;
        private DateTime _currentDate;

        public FuturesInstrument(IInstrumentName name, IInstrumentStream stream, IConnection connection)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _resolver = new TimeframeResolver(name, _stream, _connection);
            _stream.OnPriceUpdated += HandlePriceUpdated;
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;
        public event EventHandler<IPosition> OnPositionOpened;

        public IInstrumentName Name { get; }

        public decimal Price { get; private set; }
        
        public ITimeframe GetTimeframe(Timeframes.Timeframes type)
        {
            return _resolver.Resolve(type);
        }

        private void HandlePriceUpdated(object sender, IPriceTick tick)
        {
            _currentDate = tick.DateTime;
            Price = tick.Price;
            OnPriceUpdated?.Invoke(this, tick);
        }

        public void SetPositionEntry(PositionSides side,
            int leverage,
            decimal stopLoss,
            IEnumerable<(decimal Price, decimal Volume)> takeProfits,
            decimal size,
            Guid id)
        {
            var position = new VirtualPosition(takeProfits, Price, stopLoss, Name, _stream, side, leverage, size,
                _currentDate, id);
            
            OnPositionOpened?.Invoke(this, position);
        }
    }
}
