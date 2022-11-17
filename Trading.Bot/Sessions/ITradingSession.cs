using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions
{
    public interface ITradingSession
    {
        event EventHandler<IReadOnlyCollection<ISignal>> OnStopped;
        DateTime Date { get; }
        void Start();
        void Stop();
    }
}
