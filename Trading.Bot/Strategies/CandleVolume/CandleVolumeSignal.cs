using System;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeSignal : ISignal
    {
        private readonly ISignal _signal;

        public CandleVolumeSignal(IIndexedOhlcv ic, PositionSides position, IInstrumentName instrumentName, Timeframes timeframe, Strategies strategy)
        {
            _signal = new Signal(ic, position, instrumentName, new CandleVolumeRiskManagment(), 0.01m, timeframe, strategy);
        }

        public IInstrumentName InstrumentName { get => _signal.InstrumentName; }

        public decimal TakeProfit { get => _signal.TakeProfit; }

        public int Index { get => _signal.Index; }

        public decimal Price { get => _signal.Price; }

        public decimal StopLoss { get => _signal.StopLoss; }

        public DateTimeOffset Date { get => _signal.Date.UtcDateTime; }

        public PositionSides Side { get => _signal.Side; }

        public decimal RiskPercent { get => _signal.RiskPercent; }

        public Timeframes Timeframe { get => _signal.Timeframe; }

        public Strategies Strategy { get => _signal.Strategy; }
        public IIndexedOhlcv Candle { get => _signal.Candle; }

        public Guid Id { get => _signal.Id; }
    }
}
