using System;
using Trading.Analysis.Indicators;
using Trading.Bot.Strategies.CandleVolume.Filters;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Strategies.CandleVolume;

public class CandleVolumeContextParser : IContextParser<CandleVolumeStrategyContext>
{
    public CandleVolumeStrategyContext Parse(ISignal signal) 
    {
        var ic = signal.Candle;
        var pd = new PdArrayLiquidityMatrix(ic.BackingList, ic.Index);
        var grid = pd.Grid;
        var takeProfitChannelExtension = grid.GetChanelExtension(signal.TakeProfit);
        var eqDistance = grid.GetEquilibriumDistance(signal.Price);
        var pdSize = grid.Size();
        
        return new CandleVolumeStrategyContext
        {
            PdSize = Math.Round(pdSize, 4),
            Short = signal.Side == PositionSides.Short,
            DayTime = signal.Date.Hour,
            TakeProfitChannelExtension = Math.Round(takeProfitChannelExtension, 4) * 100,
            EquilibriumDistance = Math.Round(eqDistance, 4)
        };
    }
}