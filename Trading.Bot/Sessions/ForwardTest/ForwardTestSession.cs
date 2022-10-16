using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;
using Trading.Exchange;

namespace Trading.Bot.Sessions.ForwardTest
{
    internal class ForwardTestSession : ITradingSession
    {
        private readonly ITradingSession _session;

        public ForwardTestSession(IExchange exchange, Strategies.Strategies strategy)
        {
            _ = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _session = new TradingSession(new ForwardTestSessionAbstractFactory(exchange, strategy));
        }

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
