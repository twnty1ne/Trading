using Trading.Exchange.Authentification;

namespace Trading.Api
{
    public class BinanceCredentialsProvider : ICredentialsProvider
    {
        public ICredentials GetCredentials()
        {
            return new Credentials(string.Empty, string.Empty);
        }
    }
}
