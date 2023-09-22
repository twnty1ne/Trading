using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeSignal : ISignal
    {
        private readonly ISignal _signal;

        public CandleVolumeSignal(IIndexedOhlcv ic,
            PositionSides position,
            IInstrumentName instrumentName,
            Timeframes timeframe,
            Strategies strategy)
        {
            _signal = new Signal(ic, position, instrumentName, 
                new CandleVolumeRiskManagement(), 0.0001m, timeframe, strategy);
        }

        public IInstrumentName InstrumentName { get => _signal.InstrumentName; }

        public IEnumerable<(decimal Price, decimal Volume)> TakeProfits 
            => _signal.TakeProfits;

        public int Index => _signal.Index;

        public decimal Price => _signal.Price;

        public decimal StopLoss => _signal.StopLoss;

        public DateTimeOffset Date => _signal.Date.UtcDateTime;

        public PositionSides Side => _signal.Side;

        public decimal RiskPercent => _signal.RiskPercent;

        public Timeframes Timeframe => _signal.Timeframe;

        public Strategies Strategy => _signal.Strategy;
        public IIndexedOhlcv Candle => _signal.Candle;

        public Guid Id => _signal.Id;
    }
}
