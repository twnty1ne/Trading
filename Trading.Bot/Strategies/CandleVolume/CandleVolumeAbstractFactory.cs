using System;
using System.Collections.Generic;
using Trading.Analysis.Extentions;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.MlClient;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace Trading.Bot.Strategies.CandleVolume
{
    internal class CandleVolumeAbstractFactory : IStrategyAbstractFactory
    {
        private readonly IMlClient _mlClient;

        public CandleVolumeAbstractFactory(IMlClient mlClient)
        {
            _mlClient = mlClient ?? throw new ArgumentNullException(nameof(mlClient));
        }

        public Func<IIndexedOhlcv, PositionSides, IInstrumentName, Timeframes, Strategies, ISignal> SignalSelector 
            => (x, y, z, t, p) => new CandleVolumeSignal(x, y, z, t, p);

        public Predicate<IIndexedOhlcv> SellRule =>
            Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(2))
                .And(x => x.IsBreakingHighestHigh(1))
                .And(x => !x.IsBreakingLowestLow(1));

        public Predicate<IIndexedOhlcv> BuyRule =>
            Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(2))
                .And(x => x.IsBreakingLowestLow(1))
                .And(x => !x.IsBreakingHighestHigh(1));

        public IReadOnlyCollection<IInstrumentName> SupportedInstruments  => new List<IInstrumentName>
            {
                new InstrumentName("ETH", "USDT"),
                new InstrumentName("BTC", "USDT"),
                new InstrumentName("XRP", "USDT"),
                new InstrumentName("ADA", "USDT"),
                new InstrumentName("SOL", "USDT"),
                new InstrumentName("LTC", "USDT"),
                new InstrumentName("BNB", "USDT"),
                new InstrumentName("ETC", "USDT"),
                new InstrumentName("UNI", "USDT"),
                new InstrumentName("LINK", "USDT"),
                new InstrumentName("NEAR", "USDT"),
                new InstrumentName("ATOM", "USDT"),
            };

        public IReadOnlyCollection<Timeframes> SupportedTimeframes =>
            new List<Timeframes>
            {
                Timeframes.OneHour,
            };

        public Strategies Strategy => Strategies.CandleVolume;

        public ISignalsFilter Filter => new CandleVolumeStrategyFilterAdapter(_mlClient);
    }
}
