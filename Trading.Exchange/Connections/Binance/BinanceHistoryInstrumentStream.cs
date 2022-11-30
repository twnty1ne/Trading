using Binance.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Binance
{
    internal class BinanceHistoryInstrumentStream : IInstrumentStream
    {
        private readonly IBinanceSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly IConnection _connection;
        private readonly IMarketTicker _ticker;
        private IEnumerable<decimal> _prices;
        private ITimeframeStream _stream;

        public BinanceHistoryInstrumentStream(IConnection connection, IInstrumentName name, IBinanceSocketClient client, IMarketTicker ticker)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _prices = new List<decimal>();
            _stream = GetTimeframeStream(Timeframes.FiveMinutes);
            _stream.OnCandleClosed += HandleCandleClosed;
            _ticker.OnTick += HandleTick;
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;

        public ITimeframeStream GetTimeframeStream(Timeframes timeframe)
        {
            return new BinanceHistoryTimeframeStream(_connection, _name, timeframe, _ticker);
        }

        private void HandleCandleClosed(object sender, IReadOnlyCollection<ICandle> candles) 
        {
            var lastCandle = candles.Last();
            if (lastCandle.Close > lastCandle.Open) 
                _prices = new List<decimal> { lastCandle.Open, lastCandle.High, lastCandle.Low };
            if (lastCandle.Close <= lastCandle.Open) 
                _prices = new List<decimal> { lastCandle.Open, lastCandle.Low, lastCandle.High };
        }

        private void HandleTick(object sender, IMarketTick tick) 
        {
            if (_prices.Any()) 
            {
                var price = _prices.First();
                Debug.WriteLine($"Date: {tick.Date}, Price {price}");
                OnPriceUpdated?.Invoke(this, new PriceTick(price, tick.Date));
                _prices = _prices.Skip(1);
            }
        }
    }
}
