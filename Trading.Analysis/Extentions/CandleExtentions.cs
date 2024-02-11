using Trading.Analysis.Indicators;
using Trady.Analysis.Candlestick;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace Trading.Analysis.Extentions
{
    public static class CandleExtentions
    {
        public static bool IsDojiBar(this IIndexedOhlcv indexedCandle) 
        {
            return new Candlesticks.Doji(indexedCandle.BackingList)[indexedCandle.Index].Tick;
        }

        public static bool IsBreakingLowestVolume(this IIndexedOhlcv ic, int period)
        {
            var lowestVolume = new LowestVolume(ic.BackingList, period);
            return lowestVolume.Diff(ic.Index).Tick.IsNegative();
        }

        public static bool IsUptrend(this IIndexedOhlcv ic, int period) 
        {
            var upTrend = new UpTrend(ic.BackingList, period);
            return (bool)upTrend[ic.Index].Tick;
        }
    }
}
