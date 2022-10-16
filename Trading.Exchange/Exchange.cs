using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;

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
    }
}
