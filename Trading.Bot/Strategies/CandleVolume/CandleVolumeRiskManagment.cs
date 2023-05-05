using System;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeRiskManagment : IRiskManagment
    {
        private readonly decimal _riskRescue = 3m;
        private readonly decimal _stopLossPersent = 0.005m;

        public (decimal Price, decimal StopLoss, decimal TakeProfit) Calculate(IIndexedOhlcv ic, PositionSides side)
        {
            var price = ic.Close;
            var stopLoss = CalculateStopLoss(ic, side);
            return (price, stopLoss, CalculateTakeProfit(ic, side, price, stopLoss));
        }

        private decimal CalculateStopLoss(IIndexedOhlcv ic, PositionSides side)
        {
            return side == PositionSides.Short
                ? ic.Close + ic.Close * _stopLossPersent
                : ic.Close - ic.Close * _stopLossPersent;
        }

        private decimal CalculateTakeProfit(IIndexedOhlcv ic, PositionSides side, decimal price, decimal stopLoss)
        {
            var riskAbs = Math.Abs(stopLoss - price);
            
            return side == PositionSides.Short
                ? price - _riskRescue * riskAbs
                : price + _riskRescue * riskAbs;
        }
    }
}
