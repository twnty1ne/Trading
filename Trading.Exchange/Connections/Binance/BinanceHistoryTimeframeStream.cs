using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceHistoryTimeframeStream : ITimeframeStream
    {
        private readonly IInstrumentName _name;
        private readonly Timeframes _timeframe;
        private List<ICandle> _candleBuffer;
        private IReadOnlyCollection<ICandle> _closedCandles;

        public BinanceHistoryTimeframeStream(IConnection connection, IInstrumentName name, Timeframes timeframe)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _timeframe = timeframe;
            _ = connection ?? throw new ArgumentNullException(nameof(connection));
            _candleBuffer = connection.GetFuturesCandlesAsync(_name, _timeframe).GetAwaiter().GetResult().ToList();
            _closedCandles = new List<ICandle>();
            Start();
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;

        private void Start() 
        {
            Task.Run(() =>
            {
                foreach (var candle in _candleBuffer) 
                {
                    var closedCandlesCopy = new List<ICandle>(_closedCandles);
                    closedCandlesCopy.Add(candle);
                    _closedCandles = closedCandlesCopy;
                    OnCandleClosed?.Invoke(this, _closedCandles);
                }
                _candleBuffer.Clear();
            });
        }
    }
}
