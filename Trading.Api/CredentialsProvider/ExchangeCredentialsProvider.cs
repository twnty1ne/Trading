using Microsoft.Extensions.Options;
using Trading.Exchange.Authentification;

namespace Trading.Api.CredentialsProvider
{
    public class ExchangeCredentialsProvider : ICredentialsProvider
    {
        private ExchangeCredentials _options;

        public ExchangeCredentialsProvider(IOptions<ExchangeCredentials> options)
        {
            _options = options.Value;
        }

        public ICredentials GetCredentials()
        {
            return new Credentials(_options.PublicKey, _options.SecretKey);
        }
    }
}
