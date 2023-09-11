using RestSharp;
using Trading.MlClient.Resources.Models;

namespace Trading.MlClient.Resources.Models.InsideChannelShort;

public class InsideChannelShortModelResource : ModelResource<InsideChannelShortFeatures>
{
    public InsideChannelShortModelResource(string host) : base(host, "insideChannelShort")
    {
    }
}