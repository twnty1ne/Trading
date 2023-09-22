using System;
using Trading.Researching.Core;
using System.Linq;
using Trading.Researching.Core.Analytics.Metrics;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Sessions.Analytics.Metrics
{
    internal class WinLossRatioMetric : IMetric<IPosition, SessionMetrics>
    {
        private readonly IMetric<IPosition, SessionMetrics> _metric;

        public WinLossRatioMetric()
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
            return new Metric<IPosition, SessionMetrics>(CreateSelector(), SessionMetrics.WinLossRatio);
        }

        private Func<ISelection<IPosition>, decimal> CreateSelector()
        {
            return x =>
            {
                var amountOfWinEntries = Convert.ToDecimal(x.Data.Count(x => x.Result != PositionResult.Unspecified && x.Result == PositionResult.HitStopLoss));
                var amountOfLossEntries = Convert.ToDecimal(x.Data.Count(position => position.Result == PositionResult.HitStopLoss));
                if (amountOfWinEntries == 0) return decimal.Zero;
                if (amountOfLossEntries == 0) return 1m;
                return amountOfWinEntries / (amountOfLossEntries + amountOfWinEntries);
            };
        }
    }
}