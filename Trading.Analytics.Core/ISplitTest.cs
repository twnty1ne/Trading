using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analytics.Core
{
    public interface ISplitTest<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        SplitTestResult<T, R, TParameter> GetDifference();
        ISelection<TParameter, T> GetOptimal();
    }
}
