using Binance.Net.Interfaces.Clients;
using System;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceHistoryInstrumentStream : IInstrumentStream
    {
        private readonly IBinanceSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly IConnection _connection;
        private readonly IMarketTicker _ticker;

        public BinanceHistoryInstrumentStream(IConnection connection, IInstrumentName name, IBinanceSocketClient client, IMarketTicker ticker)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;

        public ITimeframeStream GetTimeframeStream(Timeframes timeframe)
        {
            return new BinanceHistoryTimeframeStream(_connection, _name, timeframe, _ticker);
        }
    }
}
