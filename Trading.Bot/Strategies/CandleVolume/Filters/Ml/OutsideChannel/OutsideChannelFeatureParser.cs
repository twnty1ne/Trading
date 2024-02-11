using System.Collections.Generic;
using Trading.Bot.Strategies.Filters.Ml;
using Trading.MlClient.Resources.Models.OutsideChannel;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml.OutsideChannel;

public class OutsideChannelFeatureParser : IFeatureParser<CandleVolumeStrategyContext, OutsideChannelFeatures>
{
    public IEnumerable<(OutsideChannelFeatures Feature, decimal Value)> Parse(CandleVolumeStrategyContext signal)
    {
        return new List<(OutsideChannelFeatures Feature, decimal Value)>
        {
            (OutsideChannelFeatures.PdSize, signal.PdSize),
            (OutsideChannelFeatures.TakeProfitChannelExtension, signal.TakeProfitChannelExtension)
        };
    }
}