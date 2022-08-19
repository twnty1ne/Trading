using System;
using System.Collections.Generic;
using Trading.Exchange.Connections;
using Trading.Shared.Resolvers;
using Trading.Infrastructure.Connections.Binance;
using Trading.Infrastructure.Exchange.Authentification;

namespace Trading.Infrastructure.Exchange.Connections
{
    internal class ConnectionReslover : IResolver<ConnectionEnum, IConnection>
    {
        private readonly IResolver<ConnectionEnum, IConnection> _resolver;
        private readonly ICredentialsProvider _credentialsProvider;

        public ConnectionReslover(ICredentialsProvider credentialProvider)
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
