using Bybit.Net.Enums;
using Bybit.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Connections.Binance.Extentions;
using Trading.Exchange.Connections.Bybit.Extentions;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Bybit
{
    internal class BybitTimeframeStream : ITimeframeStream
    {
        private readonly IBybitSocketClient _socketClient;
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;
        private IReadOnlyCollection<ICandle> _closedCandles;

        public BybitTimeframeStream(IConnection connection, IBybitSocketClient socketClient, IInstrumentName name, Timeframes timeframe)
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
            var successfullyConverted = _timeframe.TryConvertToBybitTimeframe(out convertedTimeframe);

            if (!successfullyConverted) throw new ArgumentException("Invalid timeframe");

            await _socketClient.UsdPerpetualStreams.SubscribeToKlineUpdatesAsync(_name.GetFullName(), convertedTimeframe, x =>
            {
                var streamCandle = x.Data.First();
                var candle = new Candle(streamCandle.OpenPrice, streamCandle.ClosePrice, streamCandle.HighPrice, streamCandle.LowPrice, 
                    streamCandle.Volume, streamCandle.OpenTime, streamCandle.CloseTime);

                if (streamCandle.Confirm) 
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
