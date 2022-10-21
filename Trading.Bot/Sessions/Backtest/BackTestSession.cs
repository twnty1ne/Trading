using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions.Backtest
{
    internal class BacktestSession : ITradingSession
    {
        public BacktestSession()
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
