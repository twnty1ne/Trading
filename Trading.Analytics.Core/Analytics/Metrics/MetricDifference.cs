using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.Analytics.Metrics
{
    internal class MetricDifference<T, R> : IMetric<T, R> where R : Enum
    {
        private readonly ISelection<T> _innerSelection;
        private readonly IMetric<T, R> _innerMetric;

        public MetricDifference(ISelection<T> selection, IMetric<T, R> innerMetric)
        {
            _innerSelection = selection ?? throw new ArgumentNullException(nameof(selection));
            _innerMetric = innerMetric ?? throw new ArgumentNullException(nameof(innerMetric));
        }

        public R Type { get => _innerMetric.Type; }

        public IMetricResult<R> GetResult(ISelection<T> selection)
        {
            var innerSelectionResult = _innerMetric.GetResult(_innerSelection);
            var outterSelectionResult = _innerMetric.GetResult(selection);
            return new MetricResult<R>(_innerMetric.Type, outterSelectionResult.Value - innerSelectionResult.Value);
        }
    }
}
