using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments.Positions
{
    public enum PositionStates
    {
        ClosedByTakeProfit = 1,
        InProgress = 2,
        ClosedByStopLoss = 3,
        Closed = 4

    }
}
