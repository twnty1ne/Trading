using Stateless;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trading.Shared.Ranges;

namespace Trading.Exchange.Connections.Ticker
{
    public class MarketTicker : IMarketTicker
    {
        private readonly StateMachine<TickerStates, TickerTriggers> _stateMachine;

        private readonly long _spanForTick = TimeSpan.FromSeconds(20).Ticks;
        
        private Task _task = new Task(() => throw new Exception());
        private long _accumulatedTicks;
        
        private EventHandler<IMarketTick> _onTick;
        private object _lock = new object();
        private CancellationTokenSource _tokenSource;

        public MarketTicker(IRange<DateTime> ticksRange)
        {
            TicksRange = ticksRange ?? throw new ArgumentNullException(nameof(ticksRange));
            _tokenSource = new CancellationTokenSource();
            _stateMachine = new StateMachine<TickerStates, TickerTriggers>(TickerStates.WaitingForStart);

            _stateMachine
                .Configure(TickerStates.WaitingForStart)
                .Permit(TickerTriggers.Start, TickerStates.Started)
                .OnEntryFrom(TickerTriggers.Reset, HandleReset)
                .Ignore(TickerTriggers.Reset);

            _stateMachine
                .Configure(TickerStates.Started)
                .OnEntryFrom(TickerTriggers.Start, HandleStarted)
                .Permit(TickerTriggers.Reset, TickerStates.WaitingForStart);
        }

        public event EventHandler OnStopped;
        public event EventHandler OnReset;

        public IRange<DateTime> TicksRange { get;}

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

        public void Start()
        {
            _stateMachine.Fire(TickerTriggers.Start);
        }

        public void Reset()
        {
            _stateMachine.Fire(TickerTriggers.Reset);
        }

        private void HandleStarted()
        {
            var startDate = TicksRange.From;
            var normalizedStartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, 
                0, 0);

            var ticks = (TicksRange.To - normalizedStartDate).Ticks / _spanForTick + 1;

            _task = Task.Run(() =>
            {
                while (ticks >= _accumulatedTicks && !_tokenSource.IsCancellationRequested)
                {
                    lock (_lock)
                    {
                        var tickDate = normalizedStartDate.AddTicks(_accumulatedTicks * _spanForTick);
                        _onTick?.Invoke(this, new MarketTick(tickDate));
                        _accumulatedTicks++;
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
                _accumulatedTicks = 0;
            }
        }
    }
}