using System;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Resolvers;

namespace Trading.Exchange.Markets.Core.Instruments
{
    internal class FuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IInstrumentStream _stream;
        private readonly IResolver<Timeframes.Timeframes, ITimeframe> _resolver;

        public FuturesInstrument(IInstrumentName name, IInstrumentStream stream, IConnection connection)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _resolver = new TimeframeResolver(name, _stream, _connection);
            _stream.OnPriceUpdated += HandlePriceUpdated;
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;

        public IInstrumentName Name { get; }


        public ITimeframe GetTimeframe(Timeframes.Timeframes type)
        {
            return _resolver.Resolve(type);
        }

        private void HandlePriceUpdated(object sender, IPriceTick tick) 
        {
            OnPriceUpdated?.Invoke(this, tick);
        }
    }
}
