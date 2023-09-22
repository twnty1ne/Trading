using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments.Positions
{
    internal class Position : IPosition
    {
        private readonly decimal _takeProfit;
        private readonly decimal _entryPrice;
        private readonly decimal _stopLoss;

        public Position(decimal takeProfit, decimal entryPrice, decimal stopLoss, IInstrumentName instrumentName)
        {
            _takeProfit = takeProfit;
            _entryPrice = entryPrice;
            _stopLoss = stopLoss;
            InstrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));
        }

        public event EventHandler OnClosed;
        public IInstrumentName InstrumentName { get; }
        public PositionStates State { get; private set; }
        public decimal CurrentPrice => throw new NotImplementedException();
        public int Leverage => throw new NotImplementedException();
        public decimal IMR => throw new NotImplementedException();
        public PositionSides Side => throw new NotImplementedException();
        public decimal Size => throw new NotImplementedException();
        public decimal InitialMargin => throw new NotImplementedException();
        public decimal UnrealizedPnL => throw new NotImplementedException();
        public decimal RealizedPnl => throw new NotImplementedException();
        public decimal ROE => throw new NotImplementedException();
        public IEnumerable<IPriceTick> Ticks => throw new NotImplementedException();
        public decimal TakeProfit => throw new NotImplementedException();
        public DateTime CloseDate => throw new NotImplementedException();
        public decimal EntryPrice => throw new NotImplementedException();
        public PositionResult Result => throw new NotImplementedException();
        public decimal StopLoss => throw new NotImplementedException();
        public IEnumerable<(decimal Price, decimal Volume)> TakeProfits => throw new NotImplementedException();
        public DateTime EntryDate => throw new NotImplementedException();
        public Guid Id => throw new NotImplementedException();
    }
}
