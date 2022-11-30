using System;
using Trading.Researching.Core.Analytics;

namespace Trading.Researching.Core
{
    public interface ISplitAnalytics<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        SplitAnalyticsResult<T, R, TParameter> GetDifference();
    }
}
