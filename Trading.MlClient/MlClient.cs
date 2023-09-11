using Microsoft.Extensions.Options;
using Trading.MlClient.Options;
using Trading.MlClient.Resources.Models;
using Trading.MlClient.Resources.Models.InsideChannelLong;
using Trading.MlClient.Resources.Models.InsideChannelShort;
using Trading.MlClient.Resources.Models.OutsideChannel;

namespace Trading.MlClient;

public class MlClient : IMlClient
{
    private readonly ClientOptions _options;

    public IModelResource<InsideChannelLongFeatures> InsideLongModelResource { get; }
    public IModelResource<InsideChannelShortFeatures> InsideShortModelResource { get; }
    public IModelResource<OutsideChannelFeatures> OutsideModelResource { get; }

    public MlClient(IOptions<ClientOptions> options)
    {
        _options = options.Value;
        InsideLongModelResource = new InsideChannelLongModelResource(_options.GetUrl().ToString());
        InsideShortModelResource = new InsideChannelShortModelResource(_options.GetUrl().ToString());
        OutsideModelResource = new OutsideChannelModelResource(_options.GetUrl().ToString());
    }
}