using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Exchange.Connections.Binance.Extentions;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceTimeframeSocketConnection : ITimeframeSocketConnection
    {
        private readonly IBinanceSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;

        public BinanceTimeframeSocketConnection(IBinanceSocketClient client, IInstrumentName name, Timeframes timeframe)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _timeframe = timeframe;
            ListenCandleUpdates().Wait();
        }

        public event EventHandler<ICandle> OnCandleClosed;

        private async Task ListenCandleUpdates()
        {
            KlineInterval convertedTimeframe;
            var successfullyConverted = _timeframe.TryConvertToBinanceTimeframe(out convertedTimeframe);
            if (!successfullyConverted) throw new ArgumentException("Invalid timeframe");
            await _client.UsdFuturesStreams.SubscribeToKlineUpdatesAsync(_name.GetFullName(), convertedTimeframe, x =>
            {
                var streamCandle = x.Data.Data;
                var candle = new Candle(streamCandle.OpenPrice, streamCandle.ClosePrice, streamCandle.HighPrice, streamCandle.LowPrice, 
                    streamCandle.Volume, streamCandle.OpenTime, streamCandle.CloseTime);
                if (streamCandle.Final) OnCandleClosed?.Invoke(this, candle);
            });
        }
    }
}
