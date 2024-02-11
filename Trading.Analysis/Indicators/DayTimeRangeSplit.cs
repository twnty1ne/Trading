using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Trady.Analysis;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;
using System.Globalization;

namespace Trading.Analysis.Indicators;

public class DayTimeRangeSplit : AnalyzableBase<IOhlcv, IOhlcv, IEnumerable<IOhlcv>, AnalyzableTick<IEnumerable<IOhlcv>>>
{
    public DayTimeRangeSplit(IEnumerable<IOhlcv> inputs) : base(inputs, x => x)
    {
    }

    protected override IEnumerable<IOhlcv> ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
    {
        var candle = mappedInputs[index];
        var dayOfYear = candle.DateTime.UtcDateTime.DayOfYear;
        var candles =  mappedInputs.Where(x => x.DateTime.UtcDateTime.Year == candle.DateTime.UtcDateTime.Year 
                                               && x.DateTime.UtcDateTime.DayOfYear == dayOfYear);

        return candles;
    }
}
