using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trading.Exchange.Connections.Binance.Extentions;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceTimeframeStream : ITimeframeStream
    {
        private readonly IBinanceSocketClient _socketClient;
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;
        private IReadOnlyCollection<ICandle> _closedCandles;

        public BinanceTimeframeStream(IConnection connection,  IBinanceSocketClient socketClient, IInstrumentName name, Timeframes timeframe)
        {
            _socketClient = socketClient ?? throw new ArgumentNullException(nameof(socketClient));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _timeframe = timeframe;
            _ = connection ?? throw new ArgumentNullException(nameof(connection));

            _closedCandles = connection.GetFuturesCandlesAsync(_name, _timeframe).GetAwaiter().GetResult();
            ListenCandleUpdates().Wait();
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;
        public event EventHandler<ICandle> OnCandleOpened;

        private async Task ListenCandleUpdates()
        {
            KlineInterval convertedTimeframe;

            var successfullyConverted = _timeframe.TryConvertToBinanceTimeframe(out convertedTimeframe);
            if (!successfullyConverted) throw new ArgumentException("Invalid timeframe");

            await _socketClient.UsdFuturesStreams.SubscribeToKlineUpdatesAsync(_name.GetFullName(), convertedTimeframe, x =>
            {
                var streamCandle = x.Data.Data;
                var candle = new Candle(streamCandle.OpenPrice, streamCandle.ClosePrice, streamCandle.HighPrice, streamCandle.LowPrice, 
                    streamCandle.Volume, streamCandle.OpenTime, streamCandle.CloseTime);

                if (streamCandle.Final) 
                {
                    var closedCandlesCopy = new List<ICandle>(_closedCandles);
                    closedCandlesCopy.Add(candle);
                    _closedCandles = closedCandlesCopy;

                    OnCandleClosed?.Invoke(this, closedCandlesCopy);
                }
            });
        }
    }
}
