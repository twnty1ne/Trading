using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core.Metrics
{
    public class Metric<T, R> : IMetric<T, R> where R : Enum
    {
        private readonly Func<IEnumerable<T>, decimal> _selector;
        private readonly R _type;

        public Metric(Func<IEnumerable<T>, decimal> selector, R type)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _type = type;
        }

        public IMetricResult<R> GetResult(IEnumerable<T> selection)
        {
            return new MetricResult<R>(_type, _selector.Invoke(selection));
        }
    }
}
