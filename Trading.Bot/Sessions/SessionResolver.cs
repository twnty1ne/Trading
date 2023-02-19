using System;
using System.Collections.Generic;
using Trading.Bot.Sessions.Backtest;
using Trading.Bot.Sessions.Forwardtest;
using Trading.Bot.Sessions.Realtime;
using Trading.Exchange;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Sessions
{
    internal class SessionResolver : IResolver<Sessions, ITradingSession>
    {
        private readonly IResolver<Sessions, ITradingSession> _resolver;
        private readonly IExchange _exchange;
        private readonly Strategies.Strategies _strategy;
        public SessionResolver(IExchange exchange, Strategies.Strategies strategy)
        {
            _strategy = strategy;
            _resolver = new Resolver<Sessions, ITradingSession>(GenerateDictionary());
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
        }

        public ITradingSession Resolve(Sessions item)
        {
            return _resolver.Resolve(item);
        }

        public bool TryResolve(Sessions type, out ITradingSession item)
        {
            return _resolver.TryResolve(type, out item);
        }

        private Dictionary<Sessions, Func<ITradingSession>> GenerateDictionary()
        {
            return new Dictionary<Sessions, Func<ITradingSession>>
            {
                { Sessions.ForwardTest, () => new ForwardtestSession(_exchange, _strategy) },
                { Sessions.BackTest, () => new BacktestSession(_exchange, _strategy) },
                { Sessions.RealTimeTrading, () => new RealtimeSession() } 
            };

        }
    }
}
