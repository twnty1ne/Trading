using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trading.Exchange.Authentification;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

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
        public abstract IInstrumentStream GetHistoryInstrumentStream(IInstrumentName name);
        public abstract IInstrumentStream GetInstrumentStream(IInstrumentName name);
    }
}
