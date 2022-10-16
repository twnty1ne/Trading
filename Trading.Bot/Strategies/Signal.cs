using System;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Positions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal class Signal : ISignal
    {
        private readonly (decimal EntryPrice, decimal StopLoss, decimal TakeProfit) _riskManagment;

        public Signal(IIndexedOhlcv ic, PositionSides side, IInstrumentName instrumentName, IRiskManagment riskManagment)
        {
            InstrumentName = instrumentName;
            Side = side;
            Date = ic.DateTime;
            Index = ic.Index;
            _riskManagment = riskManagment.Calculate(ic, side);
        }

        public decimal TakeProfit { get => _riskManagment.TakeProfit; }
        public int Index { get; private set; }
        public decimal Price { get => _riskManagment.EntryPrice; }
        public decimal StopLoss { get => _riskManagment.StopLoss; }
        public DateTimeOffset Date { get; }
        public PositionSides Side { get; }
        public IInstrumentName InstrumentName { get; }
    }
}
