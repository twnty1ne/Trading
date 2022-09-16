using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analysis.Analytics.Metrics;
using Trading.Analysis.Model;
using Trading.Researching.Core;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Analysis.Analytics
{
    public class StrategyAnalytics : IAnalytics<IEntry, StrategyMetrics>
    {
        private readonly IAnalytics<IEntry, StrategyMetrics> _analytics;

        public StrategyAnalytics(ISelection<IEntry> selection, IEnumerable<StrategyMetrics> metrics)
        {
            _analytics = new Analytics<IEntry, StrategyMetrics>(selection, metrics.Select(x => new MetricsResolver().Resolve(x)).ToList().AsReadOnly());
        }

        public IEnumerable<IMetricResult<StrategyMetrics>> GetResults()
        {
            return _analytics.GetResults();
        }

    }
}
