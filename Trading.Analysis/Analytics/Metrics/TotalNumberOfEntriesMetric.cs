using System;
using Trading.Analysis.Model;
using Trading.Researching.Core;
using System.Linq;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Analysis.Analytics.Metrics
{
    public class TotalNumberOfEntriesMetric : IMetric<IEntry, StrategyMetrics>
    {
        private readonly IMetric<IEntry, StrategyMetrics> _metric;

        public TotalNumberOfEntriesMetric()
        {
            _metric = CreateMetric();
        }

        public StrategyMetrics Type { get => _metric.Type; }

        public IMetricResult<StrategyMetrics> GetResult(ISelection<IEntry> selection)
        {
            return _metric.GetResult(selection);
        }

        private IMetric<IEntry, StrategyMetrics> CreateMetric()
        {
            return new Metric<IEntry, StrategyMetrics>(CreateSelector(), StrategyMetrics.TotalNumberOfEntries);
        }

        private Func<ISelection<IEntry>, decimal> CreateSelector()
        {
            return x => x.Data.Count(x => x.State == EntryState.HitStopLoss || x.State == EntryState.HitTakeProfit);
        }

    }
}
