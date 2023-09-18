using Trading.Bot.Strategies.Filters;

namespace Trading.Bot.Strategies.CandleVolume.Filters;

public class CandleVolumeOutlierFilter : Filter<CandleVolumeStrategyContext>
{
    public override bool Passes(CandleVolumeStrategyContext signal)
    {
        return signal.PdSize <= 0.07m && signal.DayTime >= 4;
    }
}