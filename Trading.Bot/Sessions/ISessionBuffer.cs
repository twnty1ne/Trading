using System.Collections.Generic;
using Trading.Bot.Sessions.Analytics.Metrics;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Researching.Core;

namespace Trading.Bot.Sessions
{
    public interface ISessionBuffer
    {
        IAnalytics<IPosition, SessionMetrics> Analytics { get; }
        IReadOnlyCollection<ISignal> Signals { get; }
        void Add(ISignal signal);

        IReadOnlyCollection<IPosition> Positions { get; }
        void Add(IPosition position);

        IReadOnlyCollection<ITrade> Trades { get; }
        void Add(ITrade position);
    }
}
