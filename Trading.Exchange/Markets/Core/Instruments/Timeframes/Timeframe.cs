using System;
using System.Collections.Generic;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;

namespace Trading.Exchange.Markets.Core.Instruments.Timeframes
{
    internal class Timeframe : ITimeframe
    {
        private readonly ITimeframeStream _stream;

        public Timeframe(IInstrumentName instrumentName, IInstrumentStream stream, Timeframes timeframe)
        {
            InstrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));                     
            Type = timeframe;
            _stream = stream.GetTimeframeStream(Type);
            _stream.OnCandleClosed += HandleCandleClosed;
        }

        public event EventHandler<IReadOnlyCollection<ICandle>> OnCandleClosed;

        public IInstrumentName InstrumentName { get; }

        public Timeframes Type { get; }

        public TimeSpan Span { get => Type.GetTimeframeTimeSpan(); }

        private void HandleCandleClosed(object sender, IReadOnlyCollection<ICandle> candles) 
        {
            OnCandleClosed?.Invoke(this, candles);
        }
    }
}
