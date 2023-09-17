using Trading.Bot.Strategies.Filters.Ml;
using Trading.MlClient;
using Trading.MlClient.Resources.Models.InsideChannelShort;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml.InsideChannelShort;

public class InsideChannelShortMlFilter : MlFilter<CandleVolumeStrategyContext, InsideChannelShortFeatures>
{
    public InsideChannelShortMlFilter(IMlClient client) 
        : base(client.InsideShortModelResource, new InsideChannelShortFeatureParser())
    {
    }
}