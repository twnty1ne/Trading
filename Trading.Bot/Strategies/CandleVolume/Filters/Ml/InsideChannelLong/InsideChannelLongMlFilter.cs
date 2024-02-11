using Trading.Bot.Strategies.Filters.Ml;
using Trading.MlClient;
using Trading.MlClient.Resources.Models;
using Trading.MlClient.Resources.Models.InsideChannelLong;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml.InsideChannelLong;

public class InsideChannelLongMlFilter : MlFilter<CandleVolumeStrategyContext, InsideChannelLongFeatures>
{
    public InsideChannelLongMlFilter(IMlClient client) 
        : base(client.InsideLongModelResource, new InsideChannelLongFeatureParser())
    {
    }
}