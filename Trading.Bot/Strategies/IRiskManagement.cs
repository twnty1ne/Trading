using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal interface IRiskManagement
    {
        (decimal Price, decimal StopLoss, IEnumerable<(decimal TakeProfit, decimal Volume)> takeProfits) Calculate(
            IIndexedOhlcv ic, PositionSides side);
    }
}
