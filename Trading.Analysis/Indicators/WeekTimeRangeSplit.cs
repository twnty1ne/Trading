using System;
using System.Linq;
using System.Collections.Generic;
using Trady.Analysis;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;
using System.Globalization;

namespace Trading.Analysis.Indicators;

public class WeekTimeRangeSplit : AnalyzableBase<IOhlcv, IOhlcv, IEnumerable<IOhlcv>, AnalyzableTick<IEnumerable<IOhlcv>>>
{
    public WeekTimeRangeSplit(IEnumerable<IOhlcv> inputs) 
        : base(inputs, x => x)
    {
    }

    protected override IEnumerable<IOhlcv> ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
    {
        var candle = mappedInputs[index];
        var xx = ISOWeek.GetWeekOfYear(candle.DateTime.UtcDateTime);
        return mappedInputs.Where(x => x.DateTime.UtcDateTime.Year == candle.DateTime.UtcDateTime.Year 
                                       && ISOWeek.GetWeekOfYear(x.DateTime.UtcDateTime) == xx);
    }
}

