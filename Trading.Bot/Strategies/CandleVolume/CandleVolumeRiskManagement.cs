using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeRiskManagement : IRiskManagement
    {
        private const decimal StopLossPercent = 0.004m;

        public (decimal Price, decimal StopLoss, IEnumerable<(decimal TakeProfit, decimal Volume)> takeProfits)
            Calculate(IIndexedOhlcv ic, PositionSides side)
        {
            var price = ic.Close;
            var stopLoss = CalculateStopLoss(ic, side);
            return (price, stopLoss, CalculateTakeProfit(ic, side, price, stopLoss));
        }

        private decimal CalculateStopLoss(IIndexedOhlcv ic, PositionSides side)
        {
            return side == PositionSides.Short
                ? ic.Close + ic.Close * StopLossPercent
                : ic.Close - ic.Close * StopLossPercent;
        }

        private IEnumerable<(decimal TakeProfit, decimal Volume)> CalculateTakeProfit(IIndexedOhlcv ic,
            PositionSides side, decimal price, decimal stopLoss)
        {
            var riskAbs = Math.Abs(stopLoss - price);
            var takeProfitsHistogram = TakeProfitsHistogram();

            return takeProfitsHistogram
                .Select(x => side == PositionSides.Short
                    ? (TakeProfit : price - x.RiskRatio * riskAbs, x.Volume)
                    : (TakeProfit : price + x.RiskRatio * riskAbs, x.Volume)
                );
        }

        private IEnumerable<(decimal RiskRatio, decimal Volume)> TakeProfitsHistogram() =>
            new List<(decimal RiskRatio, decimal Volume)>
            {
                (2m, 0.5m),
                (3.75m, 0.5m)
            };
    }
}
