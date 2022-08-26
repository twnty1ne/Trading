using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analysis.Indicators;
using Trady.Analysis.Candlestick;
using Trady.Analysis.Extension;
using Trady.Analysis.Indicator;
using Trady.Core.Infrastructure;

namespace Trading.Analysis.Extentions
{
    public static class CandleExtentions
    {
        public static bool IsDojiBar(this IIndexedOhlcv indexedCandle) 
        {
            return new Doji(indexedCandle.BackingList, 0.2m)[indexedCandle.Index].Tick;
        }

        public static bool IsBreakingLowestVolume(this IIndexedOhlcv ic, int period)
        {
            var lowestVolume = new LowestVolume(ic.BackingList, period);
            return lowestVolume.Diff(ic.Index).Tick.IsNegative();
        }

        public static bool IsUptrend(this IIndexedOhlcv ic, int period) 
        {
            var upTrend = new UpTrend(ic.BackingList, period);
            var value = (bool)upTrend[ic.Index].Tick;
            return (bool)upTrend[ic.Index].Tick;
        }

    }
}
