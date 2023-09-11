using Trading.MlClient.Resources.Models;
using Trading.MlClient.Resources.Models.InsideChannelLong;
using Trading.MlClient.Resources.Models.InsideChannelShort;
using Trading.MlClient.Resources.Models.OutsideChannel;

namespace Trading.MlClient;

public interface IMlClient
{
    public IModelResource<InsideChannelLongFeatures> InsideLongModelResource { get; }
    public IModelResource<InsideChannelShortFeatures> InsideShortModelResource { get; }
    public IModelResource<OutsideChannelFeatures> OutsideModelResource { get; }
}