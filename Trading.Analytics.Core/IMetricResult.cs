using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core
{
    public interface IMetricResult<R> where R : Enum
    {
        R Type { get; }
        decimal Value { get; } 
    }
}
