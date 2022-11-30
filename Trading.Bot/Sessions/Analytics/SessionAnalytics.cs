using System.Collections.Generic;
using System.Linq;
using Trading.Bot.Sessions.Analytics.Metrics;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Researching.Core;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Bot.Sessions.Analytics
{
    internal class StrategyAnalytics : IAnalytics<IPosition, SessionMetrics>
    {
        private readonly IAnalytics<IPosition, SessionMetrics> _analytics;

        public StrategyAnalytics(ISelection<IPosition> selection, IEnumerable<SessionMetrics> metrics)
        {
            _analytics = new Analytics<IPosition, SessionMetrics>(selection, metrics.Select(x => new SessionMetricsResolver().Resolve(x)).ToList().AsReadOnly());
        }

        public IEnumerable<IMetricResult<SessionMetrics>> GetResults()
        {
            return _analytics.GetResults();
        }

    }
}