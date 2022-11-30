using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments.Positions
{
    public interface IPosition
    {
        event EventHandler OnClosed;
        decimal TakeProfit { get; }
        DateTime EntryDate { get; }
        decimal EntryPrice { get; }
        decimal StopLoss { get; }
        IInstrumentName InstrumentName { get; }
        PositionStates State { get; }
        decimal CurrentPrice { get; }
        int Leverage { get; }
        decimal IMR { get; }
        PositionSides Side { get; }
        decimal Size { get; }
        decimal InitialMargin { get; }
        decimal UnrealizedPnL { get; }
        decimal RealizedPnl { get; }
        decimal ROE { get; }
    }
}
