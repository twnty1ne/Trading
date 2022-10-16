using Trading.Exchange.Markets.Instruments.Positions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal interface IRiskManagment
    {
        (decimal Price, decimal StopLoss, decimal TakeProfit) Calculate(IIndexedOhlcv ic, PositionSides side);
    }
}
