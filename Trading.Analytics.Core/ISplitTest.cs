using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analytics.Core
{
    public interface ISplitTest<T, R> where R : Enum
    {
        IReadOnlyCollection<MetricDifference<R>> GetDifference();
    }
}
