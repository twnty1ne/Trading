using System;
using Trading.Exchange;
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
            _replay = _factory.Market.GetReplay(new DateTime(2023, 1, 1, 0, 0, 0), 
                new DateTime(2023, 1, 31, 23, 59, 59));
        }

        public event EventHandler<ISessionBuffer> OnStopped;


        public DateTime Date { get => _session.Date; }

        public void Start()
        {
            _session.OnStopped += HandleStopped;
            _replay.OnDone += HandleReplayDone;
            _session.Start();
            _replay.Start();
        }

        public void Stop()
        {
            _session.Stop();
            _replay.Stop();
            _replay.OnDone -= HandleReplayDone;
            OnStopped = null;
        }

        private void HandleStopped(object sender, ISessionBuffer result)
        {
            OnStopped?.Invoke(this, result);
        }

        private void HandleReplayDone(object sender, EventArgs result)
        {
            Stop();
        }
    }
}