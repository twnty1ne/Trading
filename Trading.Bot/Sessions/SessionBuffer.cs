using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Sessions
{
    internal class SessionBuffer : ISessionBuffer
    {
        private readonly ConcurrentBag<ISignal> _entries = new ConcurrentBag<ISignal>(new List<ISignal>());
        private readonly ConcurrentBag<IPosition> _positions = new ConcurrentBag<IPosition>(new List<IPosition>());
        public IReadOnlyCollection<ISignal> Signals { get => _entries.ToList(); }

        public IReadOnlyCollection<IPosition> Positions { get => _positions.ToList(); }

        public void Add(ISignal signal)
        {
            _entries.Add(signal);
        }

        public void Add(IPosition position)
        {
            _positions.Add(position);
        }
    }
}
