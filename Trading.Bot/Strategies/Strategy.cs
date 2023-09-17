using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal class Strategy : IStrategy
    {
        private readonly IMarket<IFuturesInstrument> _market;
        private readonly Predicate<IIndexedOhlcv> _buyRule;
        private readonly Predicate<IIndexedOhlcv> _sellRule;
        private readonly IReadOnlyCollection<IInstrumentStrategyScope> _instrumentStrategyScopes;
        private readonly IReadOnlyCollection<Timeframes> _supportedTimeFrames;
        private readonly IReadOnlyCollection<IInstrumentName> _supportedInstruments;
        private readonly Func<IIndexedOhlcv, PositionSides, IInstrumentName, Timeframes, Strategies, ISignal> _signalSelector;
        private readonly Strategies _strategy;
        private readonly ISignalsFilter _filter;

        internal Strategy(IStrategyAbstractFactory factory, IMarket<IFuturesInstrument> market)
        {
            _ = factory ?? throw new ArgumentNullException(nameof(factory));
            _market = market ?? throw new ArgumentNullException(nameof(market));
            _buyRule = factory.BuyRule;
            _sellRule = factory.SellRule;
            _supportedInstruments = factory.SupportedInstruments;
            _supportedTimeFrames = factory.SupportedTimeframes;
            _signalSelector = factory.SignalSelector;
            _strategy = factory.Strategy;
            _instrumentStrategyScopes = CreateScopes();
            _filter = factory.Filter;
            Init();
        }

        public event EventHandler<ISignal> OnSignalFired;

        private IReadOnlyCollection<IInstrumentStrategyScope> CreateScopes()
        {
            return _supportedInstruments
                .Select(x => new InstrumentStrategyScope(_market.GetInstrument(x), _supportedTimeFrames, 
                    _buyRule, _sellRule, _signalSelector, _strategy))
                .ToList();
        }

        private void Init() 
        {
            foreach (var scope in _instrumentStrategyScopes) 
            {
                scope.OnSignalFired += HandleSignalFired;
            }
        }

        private void HandleSignalFired(object sender, ISignal signal) 
        {
            if(!_filter.Passes(signal))
                return;
            
            OnSignalFired?.Invoke(this, signal);
        }
    }
}
