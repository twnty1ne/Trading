using System;
using System.Collections.Generic;
using System.Text;
using Trady.Analysis;
using Trady.Analysis.Candlestick;
using Trady.Core.Infrastructure;
using Trading.Analysis.Extentions;
using Trady.Analysis.Extension;
using Trady.Analysis.Backtest;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Analysis.Strategies
{
    public class CandleVolumeStrategy : Strategy
    {
        public CandleVolumeStrategy(IMarket<IFuturesInstrument> market, decimal mathExpectation, decimal slTreshold) : base(market, mathExpectation, slTreshold)
        {
        }

        protected override Predicate<IIndexedOhlcv> CreateBuyRule()
        {
            return Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(3))
                .And(x => x.IsBreakingLowestLow(2));
        }

        protected override Predicate<IIndexedOhlcv> CreateSellRule()
        {
            return Rule
                .Create(x => x.IsDojiBar())
                .And(x => x.IsBreakingLowestVolume(3))
                .And(x => x.IsBreakingHighestHigh(2));
        }




    }
}
