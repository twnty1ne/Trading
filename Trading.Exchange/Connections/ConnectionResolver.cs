using System;
using System.Collections.Generic;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Binance;
using Trading.Exchange.Connections.Bybit;
using Trading.Shared.Resolvers;

namespace Trading.Exchange.Connections
{
    public class ConnectionResolver : IResolver<ConnectionEnum, IConnection>
    {
        private readonly IResolver<ConnectionEnum, IConnection> _resolver;
        private readonly ICredentialsProvider _credentialsProvider;

        public ConnectionResolver(ICredentialsProvider credentialProvider)
        {
            _credentialsProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
            _resolver = new Resolver<ConnectionEnum, IConnection>(GenerateDictionary());
        }

        public IConnection Resolve(ConnectionEnum item)
        {
            return _resolver.Resolve(item);
        }

        public bool TryResolve(ConnectionEnum type, out IConnection item)
        {
            return _resolver.TryResolve(type, out item);
        }

        private Dictionary<ConnectionEnum, Func<IConnection>> GenerateDictionary() 
        {
            return new Dictionary<ConnectionEnum, Func<IConnection>>
            {
                { ConnectionEnum.Binance, () => new BinanceConnection(_credentialsProvider)},
                { ConnectionEnum.Bybit, () => new BybitConnection(_credentialsProvider)},
            };
        }
    }
}
