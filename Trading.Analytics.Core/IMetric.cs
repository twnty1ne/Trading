using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core
{
    public interface IMetric<T, R> where R : Enum
    {
        IMetricResult<R> GetResult(IEnumerable<T> selection);
    }
}
