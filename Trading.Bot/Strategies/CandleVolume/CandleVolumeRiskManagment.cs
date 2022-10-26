using System;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeRiskManagment : IRiskManagment
    {
        private readonly decimal _riskRescue = 4m;

        public (decimal Price, decimal StopLoss, decimal TakeProfit) Calculate(IIndexedOhlcv ic, PositionSides side)
        {
            var price = ic.Close;
            var stopLoss = CalculateStopLoss(ic, side);
            return (price, stopLoss, CalculateTakeProfit(ic, side, price, stopLoss));
        }

        private decimal CalculateStopLoss(IIndexedOhlcv ic, PositionSides position)
        {
            if (position == PositionSides.Short) return ic.High;
            return ic.Low;
        }

        private decimal CalculateTakeProfit(IIndexedOhlcv ic, PositionSides side, decimal price, decimal stopLoss)
        {
            var riskAbs = Math.Abs(stopLoss - price);
            if (side == PositionSides.Short)
            {
                var v = price - _riskRescue * riskAbs;
                return price - _riskRescue * riskAbs;
            }
            return price + _riskRescue * riskAbs;
        }
    }
}
