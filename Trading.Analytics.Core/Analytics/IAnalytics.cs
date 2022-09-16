using System;
using System.Collections.Generic;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core
{
    public interface IAnalytics<T, R> where R : Enum
    {
        IEnumerable<IMetricResult<R>> GetResults();
        //IReadOnlyCollection<MetricDifference_T<R>> Differentiate(IAnalytics<T, R> anotherAnalitics);
    }
}
