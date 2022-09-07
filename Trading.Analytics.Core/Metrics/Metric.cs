using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core.Metrics
{
    public class Metric<T, R> : IMetric<T, R> where R : Enum
    {
        private readonly Func<ISelection<T>, decimal> _selector;

        public Metric(Func<ISelection<T>, decimal> selector, R type)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            Type = type;
        }

        public R Type { get; private set; }

        public IMetricResult<R> GetResult(ISelection<T> selection)
        {
            return new MetricResult<R>(Type, _selector.Invoke(selection));
        }
    }
}
