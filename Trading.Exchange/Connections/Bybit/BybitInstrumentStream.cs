using Bybit.Net.Enums;
using Bybit.Net.Interfaces.Clients;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Bybit
{
    internal class BybitInstrumentStream : IInstrumentStream
    {
        private readonly IBybitSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly IConnection _connection;

        public BybitInstrumentStream(IConnection connection, IInstrumentName name, IBybitSocketClient client)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            ListenCandleUpdates().Wait();
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;

        public ITimeframeStream GetTimeframeStream(Timeframes timeframe)
        {
            return new BybitTimeframeStream(_connection, _client, _name, timeframe);
        }

        private async Task ListenCandleUpdates()
        {
            var sub = await _client.UsdPerpetualStreams.SubscribeToKlineUpdatesAsync(_name.GetFullName(), KlineInterval.OneMinute, x =>
            {
                OnPriceUpdated?.Invoke(this, new PriceTick(x.Data.First().ClosePrice, DateTime.UtcNow));
            });
        }
    }
}
