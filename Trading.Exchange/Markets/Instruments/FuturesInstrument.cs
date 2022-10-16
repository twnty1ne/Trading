using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Trading.Exchange.Connections;
using System.Linq;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes;
using Trading.Shared.Resolvers;
using Trading.Exchange.Markets.Instruments.Positions;

namespace Trading.Exchange.Markets.Instruments
{
    internal class FuturesInstrument : IFuturesInstrument
    {
        private readonly IConnection _connection;
        private readonly IInstrumentSocketConnection _socketConnection;
        private readonly IResolver<Timeframes.Timeframes, ITimeframe> _resolver;

        public FuturesInstrument(IInstrumentName name, IConnection connection)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _socketConnection = _connection.GetInstrumentSocketConnection(name);
            _resolver = new TimeframeResolver(Name, _socketConnection, _connection);
        }

        public IInstrumentName Name { get; }

        public ITimeframe GetTimeframe(Timeframes.Timeframes type)
        {
            return _resolver.Resolve(type);
        }

    }
}
