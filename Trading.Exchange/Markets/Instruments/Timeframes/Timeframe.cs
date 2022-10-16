using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes.Extentions;

namespace Trading.Exchange.Markets.Instruments.Timeframes
{
    internal class Timeframe : ITimeframe
    {
        private readonly IConnection _connection;
        private readonly ITimeframeSocketConnection _socketConnection;
        private IReadOnlyCollection<ICandle> _closedCandles;

        public Timeframe(IInstrumentName instrumentName, IConnection connection, IInstrumentSocketConnection socketConnection, Timeframes timeframe)
        {
            InstrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));                     
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Type = timeframe;
            _closedCandles = GetCandles();
            _socketConnection = socketConnection.GetTimeframeSocketConnection(Type);
            _socketConnection.OnCandleClosed += HandleCandleClosed;
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;

        public IInstrumentName InstrumentName { get; }

        public Timeframes Type { get; }

        public TimeSpan Span { get => Type.GetTimeframeTimeSpan(); }

        public IReadOnlyCollection<ICandle> GetCandles()
        {
            return _connection.GetFuturesCandlesAsync(InstrumentName, Type).GetAwaiter().GetResult();
        }

        private void HandleCandleClosed(object sender, ICandle candle) 
        {
            var closedCandlesCopy = new List<ICandle>(_closedCandles);
            closedCandlesCopy.Add(candle);
            _closedCandles = closedCandlesCopy;
            OnCandleClosed?.Invoke(this, closedCandlesCopy);
        }
    }
}
