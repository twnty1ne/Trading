using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments.Positions
{
    public interface IPosition
    {
        event EventHandler OnClosed;
        Guid Id { get; }
        IEnumerable<(decimal Price, decimal Volume)> TakeProfits { get; }
        DateTime EntryDate { get; }
        DateTime CloseDate { get; }
        decimal EntryPrice { get; }
        PositionResult Result { get; }
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
        IEnumerable<IPriceTick> Ticks { get; }
    }
}
