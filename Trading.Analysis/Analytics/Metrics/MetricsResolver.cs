using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analysis.Model;
using Trading.Analytics.Core;
using Trading.Shared.Resolvers;

namespace Trading.Analysis.Analytics.Metrics
{
    internal class MetricsResolver : IResolver<StrategyMetrics, IMetric<IEntry, StrategyMetrics>>
    {
        private readonly IResolver<StrategyMetrics, IMetric<IEntry, StrategyMetrics>> _resolver;

        public MetricsResolver()
        {
            _resolver = new Resolver<StrategyMetrics, IMetric<IEntry, StrategyMetrics>>(GenerateDictionary());
        }

        public IMetric<IEntry, StrategyMetrics> Resolve(StrategyMetrics item)
        {
            return _resolver.Resolve(item);
        }

        public bool TryResolve(StrategyMetrics type, out IMetric<IEntry, StrategyMetrics> item)
        {
            return _resolver.TryResolve(type, out item);
        }

        private Dictionary<StrategyMetrics, Func<IMetric<IEntry, StrategyMetrics>>> GenerateDictionary()
        {
            return new Dictionary<StrategyMetrics, Func<IMetric<IEntry, StrategyMetrics>>>
            {
                { StrategyMetrics.WinLossRatio, () => new WinRateRatioMetric() },
                { StrategyMetrics.TotalNumberOfEntries, () => new TotalNumberOfEntriesMetric()}
            };

        }
    }
}
