using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;

namespace Trading.Exchange.Connections.Bybit
{
    internal class BybitHistoryTimeframeStream : ITimeframeStream
    {
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;
        private readonly long _timeframeTicks;
        private readonly IMarketTicker _ticker;
        private Dictionary<DateTime, ICandle> _candleBuffer;
        private IReadOnlyCollection<ICandle> _closedCandles;

        public BybitHistoryTimeframeStream(IConnection connection, IInstrumentName name, Timeframes timeframe, IMarketTicker ticker)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _timeframe = timeframe;
            _timeframeTicks = _timeframe.GetTimeframeTimeSpan().Ticks;
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _ = connection ?? throw new ArgumentNullException(nameof(connection));
            _candleBuffer = connection.GetFuturesCandlesAsync(_name, _timeframe).GetAwaiter().GetResult().ToDictionary(x => x.CloseTime.AddTicks(TimeSpan.TicksPerSecond));
            _closedCandles = new List<ICandle>();
            _ticker.OnTick += HandleNewTick;
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;
        public event EventHandler<ICandle> OnCandleOpened;


        private void HandleNewTick(object sender, IMarketTick tick)
        {
            var closedCandle = _candleBuffer.GetValueOrDefault(tick.Date);
            var openedCandle = _candleBuffer.GetValueOrDefault(new DateTime(tick.Date.Ticks + _timeframeTicks));

            if (closedCandle != null)
            {
                var closedCandlesCopy = new List<ICandle>(_closedCandles);
                closedCandlesCopy.Add(closedCandle);
                _closedCandles = closedCandlesCopy;
                OnCandleClosed?.Invoke(this, _closedCandles);
            }

            if (openedCandle != null)
            {
                OnCandleOpened?.Invoke(this, openedCandle);
            }
        }
    }
}