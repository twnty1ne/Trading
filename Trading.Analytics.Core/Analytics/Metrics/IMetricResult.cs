using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.Analytics.Metrics
{
    public interface IMetricResult<R> where R : Enum
    {
        R Type { get; }
        decimal Value { get; } 
    }
}
