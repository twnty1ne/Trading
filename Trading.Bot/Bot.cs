using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Trading.Bot.Sessions;
using Trading.Bot.Strategies;
using Trading.Exchange;
using Trading.Shared.Resolvers;

namespace Trading.Bot
{
    public class Bot : IBot
    {
        private readonly IExchange _exchange;
        private ITradingSession _session;
        private readonly IResolver<Sessions.Sessions, ITradingSession> _resolver;
        private readonly IOptions<Options> _options;

        public Bot(IExchange exchange, IOptions<Options> options)
        {
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _resolver = new SessionResolver(_exchange, options.Value.Strategy);
            Session = _resolver.Resolve(_options.Value.Session);
        }

        public ITradingSession Session
        {
            get
            {
                return _session;
            }

            private set
            {
                value.OnStopped += HandleSessionStopped;
                _session = value;
            }
        }

        private void HandleSessionStopped(object sender, IReadOnlyCollection<ISignal> result) 
        {
            Session = _resolver.Resolve(_options.Value.Session);
        }
    }
}
