using Binance.Net.Interfaces.Clients;
using Bybit.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Connections.Bybit
{
    internal class BybitHistoryInstrumentStream : IInstrumentStream
    {
        private readonly IBybitSocketClient _client;
        private readonly IInstrumentName _name;
        private readonly IConnection _connection;
        private readonly IMarketTicker _ticker;
        private IEnumerable<decimal> _prices;
        private ITimeframeStream _stream;

        public BybitHistoryInstrumentStream(IConnection connection, IInstrumentName name, IBybitSocketClient client, IMarketTicker ticker)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _prices = new List<decimal>();
            _stream = GetTimeframeStream(Timeframes.FiveMinutes);
            _stream.OnCandleOpened += HandleCandleOpened;
            _ticker.OnTick += HandleTick;
        }

        public event EventHandler<IPriceTick> OnPriceUpdated;

        public ITimeframeStream GetTimeframeStream(Timeframes timeframe)
        {
            return new BybitHistoryTimeframeStream(_connection, _name, timeframe, _ticker);
        }

        private void HandleCandleOpened(object sender, ICandle candle) 
        {
            if (candle.Close > candle.Open) 
                _prices = new List<decimal> { candle.Open, candle.High, candle.Low };

            if (candle.Close <= candle.Open) 
                _prices = new List<decimal> { candle.Open, candle.Low, candle.High };
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
