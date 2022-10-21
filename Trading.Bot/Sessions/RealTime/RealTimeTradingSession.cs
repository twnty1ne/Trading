using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions.Realtime
{
    internal class RealtimeTradingSession : ITradingSession
    {
        public RealtimeTradingSession()
        {
        }

        public event EventHandler<IReadOnlyCollection<ISignal>> OnStopped;

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
