using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions
{
    public interface ITradingSession
    {
        public event EventHandler<IReadOnlyCollection<ISignal>> OnStopped;
        void Start();
        void Stop();
    }
}
