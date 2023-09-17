using System;
using Trading.Bot.Strategies.CandleVolume.Filters;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Strategies.CandleVolume;

public class CandleVolumeContextParser : IContextParser<CandleVolumeStrategyContext>
{
    public CandleVolumeStrategyContext Parse(ISignal signal)
    {
        return new CandleVolumeStrategyContext
        {
            PdSize = decimal.One,
            Short = signal.Side == PositionSides.Short,
            DayTime = signal.Date.Hour,
            TakeProfitChannelExtension = decimal.Zero,
            EquilibriumDistance = decimal.Zero
        };
    }
}