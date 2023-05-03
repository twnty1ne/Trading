using System;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    public interface ISignal
    {
        Guid Id { get; }
        IInstrumentName InstrumentName { get; }
        decimal TakeProfit { get; }
        int Index { get; }
        decimal Price { get; }
        decimal StopLoss { get; }
        DateTimeOffset Date { get; }
        PositionSides Side { get; }
        decimal RiskPercent { get; }
        Timeframes Timeframe { get; }
        Strategies Strategy { get; }
        IIndexedOhlcv Candle { get; }
    }
}
