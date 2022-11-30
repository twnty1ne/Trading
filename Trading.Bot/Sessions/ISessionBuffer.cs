using System.Collections.Generic;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Sessions
{
    public interface ISessionBuffer
    {
        IReadOnlyCollection<ISignal> Signals { get; }
        void Add(ISignal signal);

        IReadOnlyCollection<IPosition> Positions { get; }
        void Add(IPosition position);
    }
}
