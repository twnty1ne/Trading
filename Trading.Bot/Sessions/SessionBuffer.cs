using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Trading.Bot.Sessions.Analytics;
using Trading.Bot.Sessions.Analytics.Metrics;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Researching.Core;
using Trading.Researching.Core.Analytics;

namespace Trading.Bot.Sessions
{
    internal class SessionBuffer : ISessionBuffer
    {
        private readonly ConcurrentBag<ISignal> _entries = new ConcurrentBag<ISignal>(new List<ISignal>());
        private readonly ConcurrentBag<IPosition> _positions = new ConcurrentBag<IPosition>(new List<IPosition>());
        private readonly ConcurrentBag<ITrade> _trades = new ConcurrentBag<ITrade>(new List<ITrade>());
        public IReadOnlyCollection<ISignal> Signals { get => _entries.ToList(); }

        public IReadOnlyCollection<IPosition> Positions { get => _positions.OrderBy(x => x.EntryDate).ToList(); }

        public IAnalytics<IPosition, SessionMetrics> Analytics { get => CreateAnalytics(); }

        public IReadOnlyCollection<ITrade> Trades { get => _trades.ToList(); }

        public void Add(ISignal signal)
        {
            _entries.Add(signal);
        }

        public void Add(IPosition position)
        {
            _positions.Add(position);
        }

        public void Add(ITrade position)
        {
            _trades.Add(position);
        }

        private IAnalytics<IPosition, SessionMetrics> CreateAnalytics()
        {
            var selection = new Selection<IPosition>(_positions);
            var metrics = new List<SessionMetrics> { SessionMetrics.TotalNumberOfPositions, SessionMetrics.WinLossRatio, SessionMetrics.TotalPnL };
            return new StrategyAnalytics(selection, metrics);
        }

    }
}
