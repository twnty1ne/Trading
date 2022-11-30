using Microsoft.Extensions.Options;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets;

namespace Trading.Exchange
{
    public class Exchange : IExchange
    {
        private readonly IConnection _connection;

        public Exchange(IOptions<Options> options, ICredentialsProvider credentialsProvider)
        {
            _connection = new ConnectionResolver(credentialsProvider).Resolve(options.Value.ConnectionType);
            Market = new Market(_connection);
        }

        public IMarket Market { get; }

        public IMarket SimulationMarket { get; }
    }
}
