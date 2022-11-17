using System;
using System.Collections.Generic;
using System.Diagnostics;
using Trading.Bot.Strategies;
using Trading.Exchange;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Replay;

namespace Trading.Bot.Sessions.Backtest
{
    internal class BacktestSession : ITradingSession
    {
        private readonly ITradingSession _session;
        private readonly BacktestSessionAbstractFactory _factory;
        private readonly IReplay _replay;

        public BacktestSession(IExchange exchange, Strategies.Strategies strategy)
        {
            _ = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _factory = new BacktestSessionAbstractFactory(exchange, strategy);
            _session = new TradingSession(_factory);
            _replay = _factory.Market.GetReplay(new DateTime(2022, 11, 14), DateTime.UtcNow);
            
        }

        public event EventHandler<IReadOnlyCollection<ISignal>> OnStopped;

        public DateTime Date { get => _session.Date; }

        public void Start()
        {
            _session.Start();
            _replay.Start();
            _session.OnStopped += HandleStopped;
            _replay.OnDone += HandleReplayDone;
        }

        public void Stop()
        {
            _session.Stop();
            _replay.Stop();
            _replay.OnDone -= HandleReplayDone;
            OnStopped = null;
        }

        private void HandleStopped(object sender, IReadOnlyCollection<ISignal> result)
        {
            OnStopped?.Invoke(this, result);
        }

        private void HandleReplayDone(object sender, EventArgs result)
        {
            Stop();
        }
    }
}
