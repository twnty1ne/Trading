using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceInstrumentSocketConnection : IInstrumentSocketConnection
    {
        private readonly IBinanceSocketClient _client;
        private readonly IInstrumentName _name;

        public BinanceInstrumentSocketConnection(IInstrumentName name, IBinanceSocketClient client)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            ListenCandleUpdates().Wait();
        }

        public event EventHandler<decimal> OnPriceUpdated;

        public ITimeframeSocketConnection GetTimeframeSocketConnection(Timeframes timeframe)
        {
            return new BinanceTimeframeSocketConnection(_client, _name, timeframe);
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
