using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Researching.Core.Analytics.Metrics;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Sessions.Analytics.Metrics
{
    internal class SessionMetricsResolver : IResolver<SessionMetrics, IMetric<IPosition, SessionMetrics>>
    {
        private readonly IResolver<SessionMetrics, IMetric<IPosition, SessionMetrics>> _resolver;

        public SessionMetricsResolver()
        {
            _resolver = new Resolver<SessionMetrics, IMetric<IPosition, SessionMetrics>>(GenerateDictionary());
        }

        public IMetric<IPosition, SessionMetrics> Resolve(SessionMetrics item)
        {
            return _resolver.Resolve(item);
        }

        public bool TryResolve(SessionMetrics type, out IMetric<IPosition, SessionMetrics> item)
        {
            return _resolver.TryResolve(type, out item);
        }

        private Dictionary<SessionMetrics, Func<IMetric<IPosition, SessionMetrics>>> GenerateDictionary()
        {
            return new Dictionary<SessionMetrics, Func<IMetric<IPosition, SessionMetrics>>>
            {
                { SessionMetrics.WinLossRatio, () => new WinLossRatioMetric() },
                { SessionMetrics.TotalNumberOfPositions, () => new TotalNumberOfPositionsMetric() }
            };

        }
    }
}