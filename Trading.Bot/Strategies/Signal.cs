using System;
using System.Collections.Generic;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies
{
    internal class Signal : ISignal
    {
        private readonly (decimal EntryPrice, decimal StopLoss, IEnumerable<(decimal Price, decimal Volume)> TakeProfits) _riskManagement;

        public Signal(IIndexedOhlcv ic, PositionSides side, IInstrumentName instrumentName,
            IRiskManagement riskManagement, decimal riskPercent, Timeframes timeframe, Strategies strategy)
        {
            Id = Guid.NewGuid();
            InstrumentName = instrumentName;
            Side = side;
            Date = ic.DateTime.UtcDateTime.Add(timeframe.GetTimeframeTimeSpan());
            Index = ic.Index;
            _riskManagement = riskManagement.Calculate(ic, side);
            RiskPercent = riskPercent;
            Timeframe = timeframe;
            Strategy = strategy;
            Candle = ic;
        }

        public IEnumerable<(decimal Price, decimal Volume)> TakeProfits => _riskManagement.TakeProfits;
        public int Index { get; private set; }
        public decimal Price => _riskManagement.EntryPrice;
        public decimal StopLoss => _riskManagement.StopLoss;
        public DateTimeOffset Date { get; }
        public PositionSides Side { get; }
        public IInstrumentName InstrumentName { get; }
        public decimal RiskPercent { get; }
        public Timeframes Timeframe { get; }
        public Strategies Strategy { get; }
        public Guid Id { get; }
        public IIndexedOhlcv Candle { get; }
    }
}
