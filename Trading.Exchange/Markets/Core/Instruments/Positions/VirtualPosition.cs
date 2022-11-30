﻿using Stateless;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.HistorySimulation;

namespace Trading.Exchange.Markets.Core.Instruments.Positions
{
    internal class VirtualPosition : IPosition
    {
        private readonly IInstrumentStream _stream;
        private readonly StateMachine<PositionStates, PositionTriggers> _stateMachine;
        private (decimal volume, decimal price) _realizedVolume;
        private decimal _unRealizedVolume;


        public VirtualPosition(decimal takeProfit, decimal entryPrice, decimal stopLoss, IInstrumentName instrumentName, IInstrumentStream stream, PositionSides side, 
            int leverage, decimal size)
        {
            TakeProfit = takeProfit;
            EntryPrice = entryPrice;
            CurrentPrice = entryPrice;
            StopLoss = stopLoss;
            _stream = stream;
            InstrumentName = instrumentName ?? throw new ArgumentNullException(nameof(instrumentName));
            Side = side;
            Leverage = leverage;
            Size = size;
            _unRealizedVolume = size;
            _realizedVolume = (0m, 0m);
            _stream.OnPriceUpdated += HandlePriceUpdated;
            _stateMachine = new StateMachine<PositionStates, PositionTriggers>(PositionStates.InProgress);

            _stateMachine
                .Configure(PositionStates.InProgress)
                .Permit(PositionTriggers.CloseByStopLoss, PositionStates.ClosedByStopLoss)
                .Permit(PositionTriggers.CloseByTakeProfit, PositionStates.ClosedByTakeProfit);

            _stateMachine
                .Configure(PositionStates.ClosedByStopLoss)
                .OnEntry(() => RealizeVolume(StopLoss));

            _stateMachine
                .Configure(PositionStates.ClosedByTakeProfit)
                .OnEntry(() => RealizeVolume(TakeProfit));

        }


        public decimal TakeProfit { get; }
        public decimal EntryPrice { get; }
        public decimal StopLoss { get; }
        public IInstrumentName InstrumentName { get; }
        public decimal CurrentPrice { get; private set; }
        public int Leverage { get; }
        public decimal IMR { get => 1m / Leverage; } 
        public PositionStates State { get => _stateMachine.State; }
        public PositionSides Side { get; }
        public decimal Size { get; }
        public decimal InitialMargin { get => Size * EntryPrice * IMR; }
        public decimal UnrealizedPnL { get => (CurrentPrice - EntryPrice) * _unRealizedVolume * (int)Side; }
        public decimal RealizedPnl { get => ((_realizedVolume.price * _realizedVolume.volume) - (_realizedVolume.volume * EntryPrice)) * (int)Side; }
        public decimal ROE { get => RealizedPnl / InitialMargin; }
        public DateTime EntryDate { get; } = DateTime.UtcNow;

        public event EventHandler OnClosed;


        private void HandlePriceUpdated(object sender, IPriceTick priceTick) 
        {
            CurrentPrice = priceTick.Price;
            if(HitStopLoss()) _stateMachine.FireAsync(PositionTriggers.CloseByStopLoss).Wait();
            if(HitTakeProfit()) _stateMachine.FireAsync(PositionTriggers.CloseByTakeProfit).Wait();

        }

        private void Close() 
        {
            OnClosed?.Invoke(this, EventArgs.Empty);
            _stream.OnPriceUpdated -= HandlePriceUpdated;
        }

        private bool HitStopLoss() 
        {
            return (CurrentPrice >= StopLoss && Side == PositionSides.Short) || (CurrentPrice <= StopLoss && Side == PositionSides.Long);
        }

        private bool HitTakeProfit()
        {
            return (CurrentPrice <= TakeProfit && Side == PositionSides.Short) || (CurrentPrice >= TakeProfit && Side == PositionSides.Long);
        }

        private void RealizeVolume(decimal price) 
        {
            _realizedVolume = (_unRealizedVolume, price);
            _unRealizedVolume = 0m;
            Close();
        }
    }
}