using System;
using Trading.Exchange;
using Trading.Exchange.Markets.Core.Replay;
using Trading.MlClient;

namespace Trading.Bot.Sessions.Backtest
{
    internal class BacktestSession : ITradingSession
    {
        private readonly ITradingSession _session;
        private readonly BacktestSessionAbstractFactory _factory;
        private readonly IReplay _replay;

        public BacktestSession(IExchange exchange, Strategies.Strategies strategy, IMlClient mlClient)
        {
            _ = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _factory = new BacktestSessionAbstractFactory(exchange, strategy, mlClient);
            _session = new TradingSession(_factory);
            _replay = _factory.Market.GetReplay();
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