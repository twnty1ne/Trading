using Stateless;
using System;
using System.Diagnostics;
using Trading.Exchange.Connections.Ticker;

namespace Trading.Exchange.Markets.Core.Replay
{
    internal class Replay : IReplay
    {
        private readonly IMarketTicker _ticker;
        private readonly DateTime _from;
        private readonly DateTime _to;
        private readonly StateMachine<ReplayStates, ReplayTriggers> _stateMachine;

        public Replay(IMarketTicker ticker, DateTime from, DateTime to)
        {
            _ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            _from = from;
            _to = to;

            _stateMachine = new StateMachine<ReplayStates, ReplayTriggers>(ReplayStates.WaitingForStart);

            _stateMachine
                .Configure(ReplayStates.WaitingForStart)
                .OnEntryFrom(ReplayTriggers.Stop, HandleStop)
                .Ignore(ReplayTriggers.Stop)
                .Permit(ReplayTriggers.Start, ReplayStates.Started);


            _stateMachine
                .Configure(ReplayStates.Started)
                .OnEntry(HandleStarted)
                .Permit(ReplayTriggers.Stop, ReplayStates.WaitingForStart);
        }

        public event EventHandler OnStarted;
        public event EventHandler OnDone;


        public void Start()
        {
            _stateMachine.Fire(ReplayTriggers.Start);
        }

        public void Stop()
        {
            _stateMachine.Fire(ReplayTriggers.Stop);
        }

        private void HandleStarted()
        {
            _ticker.OnTick += HandleTick;
            _ticker.Start(_from);
            OnStarted?.Invoke(this, EventArgs.Empty);
        }

        private void HandleStop()
        {
            _ticker.OnTick -= HandleTick;
            _ticker.Reset();
            OnStarted = null;
            OnDone = null;
        }

        private void HandleTick(object sender, IMarketTick tick)
        {
            if (tick.Date >= _to)
            {
                OnDone?.Invoke(this, EventArgs.Empty);
                Stop();
                return;
            }
            
            Debug.WriteLine($"Started: {_from}, Current: {tick.Date}");
        }
    }
}