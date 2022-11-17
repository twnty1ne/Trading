using System;
using Trading.Bot.Strategies;
using Trading.Exchange;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.HistorySimulation;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Sessions.Backtest
{
    internal class BacktestSessionAbstractFactory : ISessionAbstractFactory
    {
        private readonly IExchange _exchange;
        private readonly Strategies.Strategies _strategy;
        private readonly IResolver<Strategies.Strategies, IStrategy> _strategyResolver;

        public BacktestSessionAbstractFactory(IExchange exchange, Strategies.Strategies strategy)
        {
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _strategy = strategy;
            _strategyResolver = new StrategyResolver(Market);
        }

        public IStrategy Strategy { get => _strategyResolver.Resolve(_strategy); } 

        IMarket<IFuturesInstrument> ISessionAbstractFactory.Market { get => Market; }

        public HistorySimulationFuturesUsdtMarket Market { get => _exchange.Market.HistorySimulationFuturesUsdt; }

        public Action<ISignal> SignalFiredHandler { get => (x) => { }; }
    }

}