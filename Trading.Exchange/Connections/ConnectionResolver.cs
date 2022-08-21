using System;
using System.Collections.Generic;
using Trading.Connections.Binance;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Shared.Resolvers;

namespace Trading.Exchange.Connections
{
    internal class ConnectionResolver : IResolver<ConnectionEnum, IConnection>
    {
        private readonly IResolver<ConnectionEnum, IConnection> _resolver;
        private readonly ICredentialsProvider _credentialsProvider;

        public ConnectionResolver(ICredentialsProvider credentialProvider)
        {
            _resolver = new Resolver<ConnectionEnum, IConnection>(GenerateDictionary());
            _credentialsProvider = credentialProvider;

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
            };
        }
    }
}
