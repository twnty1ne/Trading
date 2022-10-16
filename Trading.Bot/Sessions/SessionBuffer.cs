using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions
{
    internal class SessionBuffer : ISessionBuffer
    {
        private readonly ConcurrentBag<ISignal> _entries = new ConcurrentBag<ISignal>(new List<ISignal>());
        public IReadOnlyCollection<ISignal> Signals { get => _entries.ToList(); }

        public void Add(ISignal signal)
        {
            _entries.Add(signal);
        }
    }
}
