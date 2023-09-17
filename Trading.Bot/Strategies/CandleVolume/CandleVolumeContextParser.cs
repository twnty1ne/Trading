using Trading.Bot.Strategies.CandleVolume.Filters;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Strategies.CandleVolume;

public class CandleVolumeContextParser : IContextParser<CandleVolumeStrategyContext>
{
    public CandleVolumeStrategyContext Parse(ISignal signal)
    {
        return new CandleVolumeStrategyContext
        {
            PdSize = decimal.Zero,
            EquilibriumDistance = decimal.Zero,
            DayTime = 10,
            TakeProfitChannelExtension = 20m,
            Short = signal.Side == PositionSides.Short
        };
    }
}