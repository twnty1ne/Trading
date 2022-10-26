using System;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Strategies
{
    public interface ISignal
    {
        IInstrumentName InstrumentName { get; }
        public decimal TakeProfit { get; }
        public int Index { get; }
        public decimal Price { get; }
        public decimal StopLoss { get; }
        public DateTimeOffset Date { get; }
        public PositionSides Side { get; }
    }
}
