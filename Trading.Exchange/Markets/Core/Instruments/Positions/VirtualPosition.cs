using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Connections;

namespace Trading.Exchange.Markets.Core.Instruments.Positions
{
    internal class VirtualPosition : IPosition
    {
        private readonly IInstrumentStream _stream;
        private readonly StateMachine<PositionStates, PositionTriggers> _stateMachine;
        private readonly List<IPriceTick> _ticks = new();
        private readonly List<(decimal Volume, decimal Price)> _realizedVolumes = new();
        private decimal _unRealizedVolume;
        private readonly List<(decimal Price, decimal Volume)> _unTriggeredTakeProfits;

        public VirtualPosition(IEnumerable<(decimal Price, decimal Volume)> takeProfits,
            decimal entryPrice,
            decimal stopLoss,
            IInstrumentName instrumentName,
            IInstrumentStream stream,
            PositionSides side, 
            int leverage, decimal size, DateTime entryDate, Guid id)
        {
            Id = id;
            TakeProfits = takeProfits;
            _unTriggeredTakeProfits = TakeProfits.ToList();
            EntryPrice = entryPrice;
            EntryDate = entryDate;
            CurrentPrice = entryPrice;
            StopLoss = stopLoss;
            _stream = stream;
            InstrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));
            Side = side;
            Leverage = leverage;
            Size = size;
            _unRealizedVolume = size;
            _stream.OnPriceUpdated += HandlePriceUpdated;
            _stateMachine = new StateMachine<PositionStates, PositionTriggers>(PositionStates.InProgress);


            _stateMachine
                .Configure(PositionStates.InProgress)
                .Permit(PositionTriggers.Close, PositionStates.Closed);

            _stateMachine
                .Configure(PositionStates.Closed)
                .OnEntryFrom(PositionTriggers.Close, HandleClose)
                .Ignore(PositionTriggers.Close);

        }

        public IEnumerable<(decimal Price, decimal Volume)> TakeProfits { get; }
        public decimal EntryPrice { get; }

        public PositionResult Result => SpecifyResult();
        public decimal StopLoss { get; }
        public IInstrumentName InstrumentName { get; }
        public decimal CurrentPrice { get; private set; }
        public int Leverage { get; }
        public decimal IMR => 1m / Leverage;
        public PositionStates State => _stateMachine.State;
        public PositionSides Side { get; }
        public decimal Size { get; }
        public decimal InitialMargin => Size * EntryPrice * IMR;
        public decimal UnrealizedPnL => (CurrentPrice - EntryPrice) * _unRealizedVolume * (int)Side;

        public decimal RealizedPnl => CalculateRealizedPnL();
        public decimal ROE => RealizedPnl / InitialMargin;
        public IEnumerable<IPriceTick> Ticks => _ticks;
        public DateTime CloseDate { get; private set; }
        public DateTime EntryDate { get; }
        public Guid Id { get; }

        public event EventHandler OnClosed;

        private void HandlePriceUpdated(object sender, IPriceTick priceTick) 
        {
            CurrentPrice = priceTick.Price;
            _ticks.Add(priceTick);

            if (HitStopLoss())
            {
                RealizeVolume(StopLoss, _unRealizedVolume);
            }

            if (HitTakeProfit(out var takeProfit))
            {
                _unTriggeredTakeProfits.Remove(takeProfit);
                RealizeVolume(takeProfit.Price, takeProfit.Volume * Size);
            }
        }

        private void Close()
        {
           _stateMachine.Fire(PositionTriggers.Close);
        }

        private void HandleClose()
        {
            CloseDate = _ticks.OrderBy(x => x.DateTime).Last().DateTime;
            
            OnClosed?.Invoke(this, EventArgs.Empty);
            _stream.OnPriceUpdated -= HandlePriceUpdated;
        }

        private bool HitStopLoss()
        {
            return Side == PositionSides.Short 
                ? CurrentPrice >= StopLoss 
                : CurrentPrice <= StopLoss;
        }

        private bool HitTakeProfit(out (decimal Price, decimal Volume) hitTakeProfit)
        {
            var takeProfit = Side == PositionSides.Short 
                ? _unTriggeredTakeProfits.FirstOrDefault(x => CurrentPrice <= x.Price)  
                : _unTriggeredTakeProfits.FirstOrDefault(x => CurrentPrice >= x.Price);

            hitTakeProfit = takeProfit;

            return takeProfit != default((decimal, decimal));
        }
        
        private void RealizeVolume(decimal price, decimal volume)
        {
            _realizedVolumes.Add((volume, price));
            _unRealizedVolume -= volume;
            
            if(_unRealizedVolume <= decimal.Zero)
                Close();
        }


        private decimal CalculateRealizedPnL()
        {
            var realizedQuoteVolume = _realizedVolumes.Sum(x => x.Volume * x.Price);
            var entryQuoteVolume = _realizedVolumes.Sum(x => x.Volume) * EntryPrice;
            return (realizedQuoteVolume - entryQuoteVolume) * (int)Side;
        }

        private PositionResult SpecifyResult()
        {
            if (State != PositionStates.Closed) 
                return PositionResult.Unspecified;
            
            if (_unTriggeredTakeProfits.Count == 0)
                return PositionResult.HitAllTakeProfits;
            
            if (_unTriggeredTakeProfits.Count < TakeProfits.Count()) 
                return PositionResult.HitTakeProfitsThenStopLoss;
            
            return PositionResult.HitStopLoss;
        }
    }
}