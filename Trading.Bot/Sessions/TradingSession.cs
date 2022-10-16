using Stateless;
using System;
using System.Collections.Generic;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Bot.Sessions
{
    internal class TradingSession : ITradingSession
    {
        private readonly IStrategy _strategy;
        private readonly IMarket<IFuturesInstrument> _market;
        private readonly ISessionBuffer _buffer;
        private readonly Action<ISignal> _signalFiredHandler;
        private readonly StateMachine<SessionStates, SessionTriggers> _stateMachine;

        public TradingSession(ISessionAbstractFactory factory)
        {
            _ = factory ?? throw new ArgumentNullException(nameof(factory));
            _market = factory.Market;
            _strategy = factory.Strategy;
            _signalFiredHandler = factory.SignalFiredHandler;
            _buffer = new SessionBuffer();
            _stateMachine = new StateMachine<SessionStates, SessionTriggers>(SessionStates.WaitingForStart);

            _stateMachine
                .Configure(SessionStates.WaitingForStart)
                .Permit(SessionTriggers.Start, SessionStates.Started);

            _stateMachine
                .Configure(SessionStates.Started)
                .OnEntry(() => HandleStarded())
                .Permit(SessionTriggers.Stop, SessionStates.Stopped);

            _stateMachine
                .Configure(SessionStates.Stopped)
                .OnEntry(() => HandleStopped());
        }

        public event EventHandler<IReadOnlyCollection<ISignal>> OnStopped;

        public void Start()
        {
            _stateMachine.Fire(SessionTriggers.Start);
        }

        public void Stop()
        {
            _stateMachine.Fire(SessionTriggers.Stop);
        }

        private void HandleStarded() 
        {
            _strategy.OnSignalFired += HandleSignalFired;
        }

        private void HandleStopped()
        {
            OnStopped?.Invoke(this, _buffer.Signals);
            _strategy.OnSignalFired -= HandleSignalFired;
            OnStopped = null;
        }

        private void HandleSignalFired(object sender, ISignal signal) 
        {
            _signalFiredHandler?.Invoke(signal);
            _buffer.Add(signal);
        }
    }
}
