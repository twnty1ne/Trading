using System;
using Trading.Bot.Strategies;
using Trading.Exchange;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.MlClient;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Sessions.Forwardtest
{
    internal class ForwardtestSessionAbstractFactory : ISessionAbstractFactory
    {
        private readonly IExchange _exchange;
        private readonly Strategies.Strategies _strategy;
        private readonly IResolver<Strategies.Strategies, IStrategy> _strategyResolver;

        public ForwardtestSessionAbstractFactory(IExchange exchange, Strategies.Strategies strategy, IMlClient mlClient)
        {
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _strategy = strategy;
            _strategyResolver = new StrategyResolver(Market, mlClient);
        }

        public IStrategy Strategy { get => _strategyResolver.Resolve(_strategy); } 

        public IMarket<IFuturesInstrument> Market { get => _exchange.Market.RealtimeFuturesUsdt; }

        public Action<ISignal> SignalFiredHandler { get => (x) => { }; }
    }
}
