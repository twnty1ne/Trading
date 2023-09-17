using System.Collections.Generic;
using Trading.Bot.Strategies.Filters.Ml;
using Trading.MlClient.Resources.Models.InsideChannelLong;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml.InsideChannelLong;

public class InsideChannelLongFeatureParser : IFeatureParser<CandleVolumeStrategyContext, InsideChannelLongFeatures>
{
    public IEnumerable<(InsideChannelLongFeatures Feature, decimal Value)> Parse(CandleVolumeStrategyContext signal)
    {
        return new List<(InsideChannelLongFeatures Feature, decimal Value)>
        {
            (InsideChannelLongFeatures.DayTime, signal.DayTime),
            (InsideChannelLongFeatures.EquilibriumDistance, signal.EquilibriumDistance),
            (InsideChannelLongFeatures.PdSize, signal.PdSize)

        };
    }
    
}