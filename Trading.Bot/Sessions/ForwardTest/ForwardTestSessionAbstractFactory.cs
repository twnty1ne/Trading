using System;
using Trading.Bot.Strategies;
using Trading.Bot.Strategies.CandleVolume;
using Trading.Exchange;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Sessions.ForwardTest
{
    internal class ForwardTestSessionAbstractFactory : ISessionAbstractFactory
    {
        private readonly IExchange _exchange;
        private readonly Strategies.Strategies _strategy;
        private readonly IResolver<Strategies.Strategies, IStrategy> _strategyResolver;

        public ForwardTestSessionAbstractFactory(IExchange exchange, Strategies.Strategies strategy)
        {
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _strategy = strategy;
            _strategyResolver = new StrategyResolver(Market);
        }

        public IStrategy Strategy { get => _strategyResolver.Resolve(_strategy); } 

        public IMarket<IFuturesInstrument> Market { get => _exchange.Market.FuturesUsdt; }

        public Action<ISignal> SignalFiredHandler { get => (x) => { }; }
    }
}
