﻿using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;

namespace Trading.Bot.Sessions.Realtime
{
    internal class RealtimeTradingSession : ITradingSession
    {
        public RealtimeTradingSession()
        {
        }

        public DateTime Date => throw new NotImplementedException();

        public event EventHandler<ISessionBuffer> OnStopped;

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
