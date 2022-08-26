using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Instruments
{
    internal class FuturesInstrument : IFuturesInstrument
    {
        public IInstrumentName Name { get; }
        private readonly IConnection _connection;

        public FuturesInstrument(IInstrumentName name, IConnection connection)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public ITimeframe GetTimeframe(Timeframes.Timeframes type)
        {
            return new Timeframe(Name, _connection, type);
        }
    }
}
