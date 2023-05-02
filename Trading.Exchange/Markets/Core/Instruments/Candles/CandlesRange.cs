using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;
using Trading.Shared.Ranges;
using Trady.Analysis;

namespace Trading.Exchange.Markets.Core.Instruments.Candles
{
    internal class CandlesRange
    {
        private readonly IEnumerable<ICandle> _candles;
        private readonly Timeframes.Timeframes _timeframe;

        public CandlesRange(IEnumerable<ICandle> candles, Timeframes.Timeframes timeframe)
        {
            _candles = candles ?? throw new ArgumentNullException(nameof(candles));
            _timeframe = timeframe;
        }

        public bool Consistent() 
        {
            if (!_candles.Any()) 
            {
                return true;
            }

            var timeframeTicks = _timeframe.GetTimeframeTimeSpan().Ticks;

            var ic = new IndexedCandle(_candles.Select(x =>
                new Trady.Core.Candle(new DateTimeOffset(x.OpenTime, TimeSpan.Zero), x.Open, x.High, x.Low, x.Close, x.Volume)).ToList().AsReadOnly(), 0);

            var index = 0;
            var consistent = true;

            while (index < ic.BackingList.Count() && consistent)
            {
                var current = ic.After(index);

                var prev = current?.Prev;
                var next = current?.Next;

                if (prev is null || next is null)
                {
                    index++;
                    continue;
                }

                var nextOpen = current.DateTime.AddTicks(timeframeTicks);
                var prevOpen = current.DateTime.AddTicks(-timeframeTicks);

                consistent = prev.DateTime == prevOpen && next.DateTime == nextOpen;
                index++;
            }

            return consistent;
        }

        public bool FullFilled(IRange<DateTime> range) 
        {
            var timeframeTicks = _timeframe.GetTimeframeTimeSpan().Ticks;
            var candlesInRange = _candles.Where(x => range.Contains(x.OpenTime) && range.Contains(x.CloseTime)).ToList();

            var candlesInRangeExpectedAmount = Math.Abs(range.From.Ticks - range.To.Ticks) / timeframeTicks;
            var allowedTreshold = new Range<long>(0, 1);
            
            return allowedTreshold.Contains(candlesInRange.Count() - candlesInRangeExpectedAmount);
        }
    }
}
