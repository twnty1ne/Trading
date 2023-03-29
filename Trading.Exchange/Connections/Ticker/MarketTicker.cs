using Stateless;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Trading.Exchange.Connections.Ticker
{
    public class MarketTicker : IMarketTicker
    {
        private readonly StateMachine<TickerStates, TickerTriggers> _stateMachine;
        private readonly StateMachine<TickerStates, TickerTriggers>.TriggerWithParameters<DateTime> _startTrigger;

        private DateTime _startDate;
        private Task _task = new Task(() => throw new Exception());
        private long _accumalutedTicks;
        private long _spanForTick = TimeSpan.FromSeconds(100).Ticks;
        private EventHandler<IMarketTick> _onTick;
        private object _lock = new object();
        private CancellationTokenSource _tokenSource;

        public MarketTicker()
        {
            _tokenSource = new CancellationTokenSource();
            _stateMachine = new StateMachine<TickerStates, TickerTriggers>(TickerStates.WaitingForStart);
            _startTrigger = _stateMachine.SetTriggerParameters<DateTime>(TickerTriggers.Start);

            _stateMachine
                .Configure(TickerStates.WaitingForStart)
                .Permit(TickerTriggers.Start, TickerStates.Started)
                .OnEntryFrom(TickerTriggers.Reset, HandleReset);

            _stateMachine
                .Configure(TickerStates.Started)
                .OnEntryFrom(_startTrigger, HandleStarded)
                .Permit(TickerTriggers.Reset, TickerStates.WaitingForStart);
        }

        public event EventHandler OnStopped;
        public event EventHandler OnReset;

        public event EventHandler<IMarketTick> OnTick
        {
            add
            {
                if (_onTick is null || !_onTick.GetInvocationList().Contains(value)) _onTick += value;
            }
            remove
            {
                _onTick -= value;
            }
        }

        public void Start(DateTime from)
        {
            _stateMachine.Fire(_startTrigger, from);
        }

        public void Reset()
        {
            _stateMachine.Fire(TickerTriggers.Reset);
        }

        private void HandleStarded(DateTime date)
        {
            _startDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

            var ticks = (DateTime.UtcNow - _startDate).Ticks / _spanForTick + 1;

            _task = Task.Run(() =>
            {
                while (ticks >= _accumalutedTicks && !_tokenSource.IsCancellationRequested)
                {
                    lock (_lock)
                    {
                        var tickDate = _startDate.AddTicks(_accumalutedTicks * _spanForTick);
                        _onTick?.Invoke(this, new MarketTick(tickDate));
                        _accumalutedTicks++;
                    }
                }
            });

            _task.Wait();

            Reset();
        }

        private void HandleReset()
        {
            lock (_lock)
            {
                _tokenSource.Cancel();
                _task.Dispose();
                _tokenSource.Dispose();
                _tokenSource = new CancellationTokenSource();
                OnReset?.Invoke(this, EventArgs.Empty);
                _accumalutedTicks = 0;
            }
        }
    }
}