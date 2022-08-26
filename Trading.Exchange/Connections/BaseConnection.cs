using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Connections
{
    public abstract class BaseConnection : IConnection
    {
        protected readonly ICredentials Credentials;
        private readonly ICredentialsProvider _credentialsProvider;

        protected BaseConnection(ICredentialsProvider credentialProvider, ConnectionEnum type)
        {
            _credentialsProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
            Credentials = _credentialsProvider.GetCredentials();
        }

        public abstract Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe);
    }
}
