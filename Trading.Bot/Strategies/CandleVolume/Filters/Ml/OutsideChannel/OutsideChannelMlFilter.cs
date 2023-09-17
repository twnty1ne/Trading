using Trading.Bot.Strategies.Filters.Ml;
using Trading.MlClient;
using Trading.MlClient.Resources.Models.OutsideChannel;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml.OutsideChannel;

public class OutsideChannelMlFilter : MlFilter<CandleVolumeStrategyContext, OutsideChannelFeatures>
{
    public OutsideChannelMlFilter(IMlClient client) 
        : base(client.OutsideModelResource, new OutsideChannelFeatureParser())
    {
    }
}