using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Positions;
using Trading.Exchange.Markets.Instruments.Timeframes;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal interface IStrategyAbstractFactory
    {
        Func<IIndexedOhlcv, PositionSides, IInstrumentName, ISignal> SignalSelector { get; }
        Predicate<IIndexedOhlcv> SellRule { get; }
        Predicate<IIndexedOhlcv> BuyRule { get; }
        IReadOnlyCollection<IInstrumentName> SupportedInstruments { get; }
        IReadOnlyCollection<Timeframes> SupportedTimeframes { get; }
    }
}
