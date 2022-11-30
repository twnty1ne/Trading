using System;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Strategies
{
    public interface ISignal
    {
        IInstrumentName InstrumentName { get; }
        decimal TakeProfit { get; }
        int Index { get; }
        decimal Price { get; }
        decimal StopLoss { get; }
        DateTimeOffset Date { get; }
        PositionSides Side { get; }
        decimal RiskPercent { get; }
    }
}
