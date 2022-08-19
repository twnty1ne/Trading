using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Exchange
{
    public class Exchange : IExchange
    {
        public IMarket Market { get; private set; }

        private readonly IConnection _connection;

        public Exchange(IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Market = new MarketRoot(_connection);

        }
    }
}
