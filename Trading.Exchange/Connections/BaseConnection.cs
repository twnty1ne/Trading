using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Connections
{
    public abstract class BaseConnection : IConnection
    {
        protected readonly ICredentials Credentials;

        protected BaseConnection(ICredentialsProvider credentialProvider, ConnectionEnum type)
        {
            _ = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
            Credentials = credentialProvider.GetCredentials();
        }

        public abstract ConnectionEnum Type { get; } 

        public abstract Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe);
        public abstract Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe, IRange<DateTime> range);
        public abstract IInstrumentStream GetHistoryInstrumentStream(IInstrumentName name, IMarketTicker ticker);
        public abstract IInstrumentStream GetInstrumentStream(IInstrumentName name);
        
    }
}
