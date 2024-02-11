using System;
using System.Collections.Generic;
using Trady.Core.Infrastructure;
using System.Linq;
using Trading.Shared.Ranges;

namespace Trading.Analysis.Indicators
{
    public class PdArrayLiquidityMatrix
    {
        private readonly IEnumerable<IOhlcv> _candles;

        public PdArrayLiquidityMatrix(IEnumerable<IOhlcv> candles, int index)
        {
            _candles = new DayTimeRangeSplit(candles)[index].Tick;
            Grid = new FibonacciGrid(new Range<decimal>(_candles.Min(x => x.Low), _candles.Max(x => x.High)));
        }

        public FibonacciGrid Grid { get; }
    }
}
