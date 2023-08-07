using System;
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
            Market = new Market(_connection, options.Value.HistorySimulationOptions.SimulationRange, 
                options.Value.RealtimeOptions.CandleBuffer);
        }

        public IMarket Market { get; }
    }
}
