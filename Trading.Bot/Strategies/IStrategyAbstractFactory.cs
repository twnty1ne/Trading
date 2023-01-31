using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal interface IStrategyAbstractFactory
    {
        Func<IIndexedOhlcv, PositionSides, IInstrumentName, Timeframes, Strategies, ISignal> SignalSelector { get; }
        Predicate<IIndexedOhlcv> SellRule { get; }
        Predicate<IIndexedOhlcv> BuyRule { get; }
        IReadOnlyCollection<IInstrumentName> SupportedInstruments { get; }
        IReadOnlyCollection<Timeframes> SupportedTimeframes { get; }
        Strategies Strategy { get; }
    }
}
