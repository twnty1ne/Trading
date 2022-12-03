using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes.Extentions;

namespace Trading.Exchange.Markets.Instruments.Timeframes
{
    internal class Timeframe : ITimeframe
    {
        private readonly IConnection _connection;

        public IInstrumentName InstrumentName { get; }
        public TimeframeEnum Type { get; }
        public TimeSpan Span  { get => Type.GetTimeframeTimeSpan(); }

        public Timeframe(IInstrumentName instrumentName, IConnection connection, TimeframeEnum timeframe)
        {
            InstrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));                     
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Type = timeframe;
        }

        public IReadOnlyCollection<ICandle> GetCandles()
        {
            return _connection.GetFuturesCandlesAsync(InstrumentName, Type).GetAwaiter().GetResult();
        }
    }
}
