using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analysis.Analytics.Metrics;
using Trading.Analysis.Model;
using Trading.Analytics.Core;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analysis.Analytics
{
    public class StrategyAnalytics : IAnalytics<IEntry, StrategyMetrics>
    {
        private readonly IAnalytics<IEntry, StrategyMetrics> _analytics;

        public StrategyAnalytics(ISelection<IEntry> selection, IEnumerable<StrategyMetrics> metrics)
        {
            _analytics = new Analytics<IEntry, StrategyMetrics>(selection, metrics.Select(x => new MetricsResolver().Resolve(x)).ToList().AsReadOnly());
        }

        public IReadOnlyCollection<MetricDifference_T<StrategyMetrics>> Differentiate(IAnalytics<IEntry, StrategyMetrics> anotherAnalitics)
        {
            return _analytics.Differentiate(anotherAnalitics);
        }

        public IEnumerable<IMetricResult<StrategyMetrics>> GetResults()
        {
            return _analytics.GetResults();
        }

    }
}
