using System.Collections.Generic;
using Trading.Bot.Strategies.Filters.Ml;
using Trading.MlClient.Resources.Models.InsideChannelShort;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml.InsideChannelShort;

public class InsideChannelShortFeatureParser : IFeatureParser<CandleVolumeStrategyContext, InsideChannelShortFeatures>
{
    public IEnumerable<(InsideChannelShortFeatures Feature, decimal Value)> Parse(CandleVolumeStrategyContext signal)
    {
        return new List<(InsideChannelShortFeatures Feature, decimal Value)>
        {
            (InsideChannelShortFeatures.DayTime, signal.DayTime),
            (InsideChannelShortFeatures.PdSize, signal.PdSize),
            (InsideChannelShortFeatures.EquilibriumDistance, signal.EquilibriumDistance)

        };
    }
    
}