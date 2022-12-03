using Stateless;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Bot.Sessions
{
    internal class TradingSession : ITradingSession
    {
        private readonly IStrategy _strategy;
        private readonly IMarket<IFuturesInstrument> _market;
        private readonly ISessionBuffer _buffer;
        private readonly Action<ISignal> _signalFiredHandler;
        private readonly StateMachine<SessionStates, SessionTriggers> _stateMachine;

        public DateTime Date { get; private set; }

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

        public event EventHandler<ISessionBuffer> OnStopped;

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
            Date = DateTime.UtcNow;
            _strategy.OnSignalFired += HandleSignalFired;
        }

        private void HandleStopped()
        {
            OnStopped?.Invoke(this, _buffer);
            _strategy.OnSignalFired -= HandleSignalFired;
            OnStopped = null;
        }

        private void HandlePositionOpened(object sender, IPosition position) 
        {
            _buffer.Add(position);
        }

        private void HandleSignalFired(object sender, ISignal signal) 
        {
            _signalFiredHandler?.Invoke(signal);
            var instrument = _market.GetInstrument(signal.InstrumentName);
            if (!_buffer.Signals.Any(x => x.InstrumentName == signal.InstrumentName)) instrument.OnPositionOpened += HandlePositionOpened;
            var price = instrument.Price;
            var volume = (_market.Balance.NetVolume * signal.RiskPercent) / Math.Abs(price - signal.StopLoss);
            instrument.SetPositionEntry(signal.Side, 30, signal.StopLoss, signal.TakeProfit, volume);
            _buffer.Add(signal);
        }
    }
}
