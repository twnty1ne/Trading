using System;
using System.Linq;
using System.Collections.Generic;
using Trady.Core.Infrastructure;
using Trady.Core;
using Trady.Analysis;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Candles;

namespace Trading.Bot.Strategies
{
    internal class InstrumentStrategyScope : IInstrumentStrategyScope
    {
        private readonly IFuturesInstrument _instrument;
        private readonly Predicate<IIndexedOhlcv> _buyRule;
        private readonly Predicate<IIndexedOhlcv> _sellRule;
        private readonly IReadOnlyCollection<Timeframes> _timeframes;
        private readonly Func<IIndexedOhlcv, PositionSides, IInstrumentName, ISignal> _signalSelector;

        public InstrumentStrategyScope(IFuturesInstrument instrument, IReadOnlyCollection<Timeframes> timeframes, 
            Predicate<IIndexedOhlcv> buyRule, Predicate<IIndexedOhlcv> sellRule, Func<IIndexedOhlcv, PositionSides, IInstrumentName, ISignal> entrySelector)
        {
            _instrument = instrument ?? throw new ArgumentNullException(nameof(instrument));
            _buyRule = buyRule ?? throw new ArgumentNullException(nameof(buyRule));
            _sellRule = sellRule ?? throw new ArgumentNullException(nameof(sellRule));
            _signalSelector = entrySelector ?? throw new ArgumentNullException(nameof(entrySelector));
            _timeframes = timeframes ?? throw new ArgumentNullException(nameof(timeframes));
            Init();
        }

        public event EventHandler<ISignal> OnSignalFired;

        private void Init() 
        {
            var timeFrames = _timeframes.Select(x => _instrument.GetTimeframe(x));
            foreach (var timeFrame in timeFrames)
            {
                timeFrame.OnCandleClosed += HandleCandleClose;
            }
        }

        private void HandleCandleClose(object sender, IReadOnlyCollection<ICandle> candles) 
        {
            using var analyzeContext = new AnalyzeContext(candles.Select(x => new Candle(new DateTimeOffset(x.OpenTime), x.Open, x.High, x.Low, x.Close, x.Volume)));
            var shortEntryCandles = new SimpleRuleExecutor(analyzeContext, _sellRule).Execute(candles.Count() - 1);
            var longEntryCandles = new SimpleRuleExecutor(analyzeContext, _buyRule).Execute(candles.Count() - 1);
            if (shortEntryCandles.Any()) OnSignalFired?.Invoke(this, _signalSelector.Invoke(shortEntryCandles.First(), PositionSides.Short, _instrument.Name));
            if (longEntryCandles.Any()) OnSignalFired?.Invoke(this, _signalSelector.Invoke(longEntryCandles.First(), PositionSides.Long, _instrument.Name));
        }
    }
}
