using System;
using System.Collections.Generic;
using Trading.Bot.Strategies.CandleVolume;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Strategies
{
    internal class StrategyResolver : IResolver<Strategies, IStrategy>
    {
        private readonly IResolver<Strategies, IStrategy> _resolver;
        private readonly IMarket<IFuturesInstrument> _market;
        public StrategyResolver(IMarket<IFuturesInstrument> market)
        {
            _market = market ?? throw new ArgumentNullException(nameof(market));
            _resolver = new Resolver<Strategies, IStrategy>(GenerateDictionary());
        }

        public IStrategy Resolve(Strategies type)
        {
           return _resolver.Resolve(type);
        }

        public bool TryResolve(Strategies type, out IStrategy item)
        {
            return _resolver.TryResolve(type, out item);
        }

        private Dictionary<Strategies, Func<IStrategy>> GenerateDictionary()
        {
            return new Dictionary<Strategies, Func<IStrategy>>
            {
                { Strategies.CandleVolume, () => new CandleVolumeStrategy(_market)},
            };
        }
    }
}
