using System;
using System.Collections.Generic;
using Trading.Analysis.Extentions;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeAbstractFactory : IStrategyAbstractFactory
    {
        public Func<IIndexedOhlcv, PositionSides, IInstrumentName, Timeframes, Strategies, ISignal> SignalSelector { get => (x, y, z, t, p) => new CandleVolumeSignal(x, y, z, t, p); }

        public Predicate<IIndexedOhlcv> SellRule 
        {
            get => Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(2))
                .And(x => x.IsBreakingHighestHigh(1))
                .And(x => !x.IsBreakingLowestLow(1));
        }

        public Predicate<IIndexedOhlcv> BuyRule
        {
            get => Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(2))
                .And(x => x.IsBreakingLowestLow(1))
                .And(x => !x.IsBreakingHighestHigh(1));
        }

        public IReadOnlyCollection<IInstrumentName> SupportedInstruments
        {
            get => new List<IInstrumentName>
            {
                new InstrumentName("ETH", "USDT"),
            };
        }

        public IReadOnlyCollection<Timeframes> SupportedTimeframes 
        {
            get => new List<Timeframes>
            {
                Timeframes.OneHour,
            };
        }

        public Strategies Strategy { get => Strategies.CandleVolume; }
    }
}
