using RestSharp;

namespace Trading.MlClient.Resources.Models.OutsideChannel;

public class OutsideChannelModelResource : ModelResource<OutsideChannelFeatures>
{
    public OutsideChannelModelResource(string host) : base(host, "outsideChannel")
    {
    }
}