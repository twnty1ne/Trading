using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Connections;
using Trading.Infrastructure.Connections.Binance;
using Trading.Infrastructure.Exchange.Authentification;

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
