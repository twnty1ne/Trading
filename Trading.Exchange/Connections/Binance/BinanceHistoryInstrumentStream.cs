using Binance.Net.Interfaces.Clients;
using System;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceHistoryInstrumentStream : IInstrumentStream
    {
        private readonly IBinanceSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly IConnection _connection;

        public BinanceHistoryInstrumentStream(IConnection connection, IInstrumentName name, IBinanceSocketClient client)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public event EventHandler<decimal> OnPriceUpdated;

        public ITimeframeStream GetTimeframeStream(Timeframes timeframe)
        {
            return new BinanceHistoryTimeframeStream(_connection, _name, timeframe);
        }
    }
}
