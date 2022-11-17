﻿using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;
using Trading.Exchange;

namespace Trading.Bot.Sessions.Forwardtest
{
    internal class ForwardtestSession : ITradingSession
    {
        private readonly ITradingSession _session;

        public ForwardtestSession(IExchange exchange, Strategies.Strategies strategy)
        {
            _ = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _session = new TradingSession(new ForwardtestSessionAbstractFactory(exchange, strategy));
        }

        public DateTime Date => throw new NotImplementedException();

        public event EventHandler<IReadOnlyCollection<ISignal>> OnStopped;

        public void Start()
        {
            _session.Start();
            _session.OnStopped += HandleStopped;
        }

        public void Stop()
        {
            _session.Stop();
            OnStopped = null;
        }

        private void HandleStopped(object sender, IReadOnlyCollection<ISignal> result) 
        {
            OnStopped?.Invoke(this, result);
        }
    }
}
