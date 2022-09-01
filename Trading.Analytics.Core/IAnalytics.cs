using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analytics.Core
{
    public interface IAnalytics<T, R> where R : Enum
    {
        IEnumerable<IMetricResult<R>> GetResults();
        IReadOnlyCollection<MetricDifference<R>> Differentiate(IAnalytics<T, R> anotherAnalitics);
    }
}
