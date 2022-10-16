using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analysis.Extentions;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Positions;
using Trading.Exchange.Markets.Instruments.Timeframes;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeAbstractFactory : IStrategyAbstractFactory
    {
        public Func<IIndexedOhlcv, PositionSides, IInstrumentName, ISignal> SignalSelector { get => (x, y, z) => new CandleVolumeSignal(x, y, z); }

        public Predicate<IIndexedOhlcv> SellRule 
        {
            get => Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(3))
                .And(x => x.IsBreakingHighestHigh(2));
        }

        public Predicate<IIndexedOhlcv> BuyRule
        {
            get => Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(3))
                .And(x => x.IsBreakingLowestLow(2));
        }

        public IReadOnlyCollection<IInstrumentName> SupportedInstruments
        {
            get => new List<IInstrumentName>
            {
                new InstrumentName("XRP", "USDT"),
            };
        }

        public IReadOnlyCollection<Timeframes> SupportedTimeframes 
        {
            get => new List<Timeframes>
            {
                Timeframes.FiveMinutes,
            };
        }
    }
}
