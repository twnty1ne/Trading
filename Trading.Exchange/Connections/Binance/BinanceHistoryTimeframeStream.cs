using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceHistoryTimeframeStream : ITimeframeStream
    {
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;
        private readonly IMarketTicker _ticker;
        private List<ICandle> _candleBuffer;
        private IReadOnlyCollection<ICandle> _closedCandles; 

        public BinanceHistoryTimeframeStream(IConnection connection, IInstrumentName name, Timeframes timeframe, IMarketTicker ticker)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _timeframe = timeframe;
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _ = connection ?? throw new ArgumentNullException(nameof(connection));
            _candleBuffer = connection.GetFuturesCandlesAsync(_name, _timeframe).GetAwaiter().GetResult().ToList();
            _closedCandles = new List<ICandle>();
            _ticker.OnTick += HandleNewTick; 
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;
        public event EventHandler<ICandle> OnCandleOpened;

        private void HandleNewTick(object sender, IMarketTick tick) 
        {
            if (_candleBuffer.Any(x => x.CloseTime.Ticks + TimeSpan.FromMilliseconds(1).Ticks == tick.Date.Ticks)) 
            { 
                var closedCandle = _candleBuffer.First(x => x.CloseTime.Ticks + TimeSpan.FromMilliseconds(1).Ticks == tick.Date.Ticks);
                var closedCandlesCopy = new List<ICandle>(_closedCandles);
                closedCandlesCopy.Add(closedCandle);
                _closedCandles = closedCandlesCopy;
                OnCandleClosed?.Invoke(this, _closedCandles);
            }
            if (_candleBuffer.Any(x => x.OpenTime == tick.Date)) 
            {
                OnCandleOpened?.Invoke(this, _candleBuffer.First(x => x.OpenTime == tick.Date));
            } 
        }
    }
}
//