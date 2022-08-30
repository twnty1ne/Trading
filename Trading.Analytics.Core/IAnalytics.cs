using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core
{
    public interface IAnalytics<T, R> where R : Enum
    {
        IEnumerable<IMetricResult<R>> GetResults();
    }
}
