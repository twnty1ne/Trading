using System.Collections.Generic;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions
{
    public interface ISessionBuffer
    {
        IReadOnlyCollection<ISignal> Signals { get; }
        void Add(ISignal signal);
    }
}
