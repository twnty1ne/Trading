using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using System;
using System.Threading.Tasks;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceInstrumentStream : IInstrumentStream
    {
        private readonly IBinanceSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly IConnection _connection;

        public BinanceInstrumentStream(IConnection connection, IInstrumentName name, IBinanceSocketClient client)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            ListenCandleUpdates().Wait();
        }

        public event EventHandler<decimal> OnPriceUpdated;

        public ITimeframeStream GetTimeframeStream(Timeframes timeframe)
        {
            return new BinanceTimeframeStream(_connection, _client, _name, timeframe);
        }

        private async Task ListenCandleUpdates()
        {
            var sub = await _client.UsdFuturesStreams.SubscribeToKlineUpdatesAsync(_name.GetFullName(), KlineInterval.OneMinute, x =>
            {
                OnPriceUpdated?.Invoke(this, x.Data.Data.ClosePrice);
            });
        }
    }
}
