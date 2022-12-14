using System;
using Trading.Researching.Core;
using System.Linq;
using Trading.Researching.Core.Analytics.Metrics;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Sessions.Analytics.Metrics
{
    internal class TotalPnLMetric : IMetric<IPosition, SessionMetrics>
    {
        private readonly IMetric<IPosition, SessionMetrics> _metric;

        public TotalPnLMetric()
        {
            _metric = CreateMetric();
        }

        public SessionMetrics Type { get => _metric.Type; }

        public IMetricResult<SessionMetrics> GetResult(ISelection<IPosition> selection)
        {
            return _metric.GetResult(selection);
        }

        private IMetric<IPosition, SessionMetrics> CreateMetric()
        {
            return new Metric<IPosition, SessionMetrics>(CreateSelector(), SessionMetrics.TotalPnL);
        }

        private Func<ISelection<IPosition>, decimal> CreateSelector()
        {
            return x => x.Data.Sum(x => x.RealizedPnl);
        }
    }
}
