using System;
using System.Collections.Generic;
using System.Text;
using Trady.Analysis;
using Trady.Analysis.Indicator;
using Trady.Core.Infrastructure;

namespace Trading.Analysis.Indicators
{
    public class LowestVolume: Lowest<IOhlcv, AnalyzableTick<decimal?>>
    {
        public LowestVolume(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Volume, periodCount)
        {
        }
    }
}
