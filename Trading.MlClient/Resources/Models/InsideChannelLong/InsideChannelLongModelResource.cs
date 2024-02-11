using RestSharp;

namespace Trading.MlClient.Resources.Models.InsideChannelLong;

public class InsideChannelLongModelResource : ModelResource<InsideChannelLongFeatures>
{
    public InsideChannelLongModelResource(string host) : base(host, "insideChannelLong")
    {
    }
}