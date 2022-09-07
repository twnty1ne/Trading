using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core.Metrics
{
    public class MetricDifference_T<R> where R : Enum
    {
        public MetricDifference_T(IMetricResult<R> leftMetric, IMetricResult<R> rightMetric)
        {
            LeftMetric = leftMetric ?? throw new ArgumentNullException(nameof(leftMetric));
            RightMetric = rightMetric ?? throw new ArgumentNullException(nameof(rightMetric));
        }

        public IMetricResult<R> LeftMetric { get; private set; }
        public IMetricResult<R> RightMetric { get; private set; }

        public decimal LeftFromRightDifference { get => LeftMetric.Value - RightMetric.Value; }
        public decimal RightFromLeftFromRightDifference { get => RightMetric.Value - LeftMetric.Value; }
    }
}
