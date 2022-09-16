using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.Analytics.Metrics
{
    public class MetricResult<R> : IMetricResult<R> where R : Enum
    {
        public MetricResult(R type, decimal value)
        {
            Type = type;
            Value = value;
        }

        public R Type { get; private set; }

        public decimal Value { get; private set; }
    }
}
