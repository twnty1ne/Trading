using System;
using System.Collections.Generic;
using Trady.Core.Infrastructure;
using System.Linq;
using Trading.Shared.Ranges;

namespace Trading.Analysis.Indicators
{
    public class PDArrayLiquidityMatrix
    {
        private readonly IEnumerable<IOhlcv> _candles;

        internal PDArrayLiquidityMatrix(IEnumerable<IOhlcv> candles, int index)
        {
            _candles = new WeekTimeRangeSplit(candles)[index].Tick;
            Grid = new FibonacciGrid(new Range<decimal>(_candles.Max(x => x.High), _candles.Min(x => x.Low)));
        }

        public FibonacciGrid Grid { get; }
    }
}
