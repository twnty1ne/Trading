using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analytics.Core.SplitTesting
{
    public class SplitTest<T, R> where R : Enum
    {
        private readonly IReadOnlyCollection<IMetric<T, R>> _metrics;
        private readonly IReadOnlyCollection<T> _leftSideSelection;
        private readonly IReadOnlyCollection<T> _rightSideSelection;

        public SplitTest(IReadOnlyCollection<IMetric<T, R>> metrics, IReadOnlyCollection<T> leftSideSelection, IReadOnlyCollection<T> rightSideSelection)
        {
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
            _leftSideSelection = leftSideSelection ?? throw new ArgumentNullException(nameof(leftSideSelection));
            _rightSideSelection = rightSideSelection ?? throw new ArgumentNullException(nameof(rightSideSelection));
        }


        public IReadOnlyCollection<MetricDifference<R>> GetDifference() 
        {
            var leftSideAnalytics = new Analytics<T, R>(_leftSideSelection, _metrics);
            var rightSideAnalytics = new Analytics<T, R>(_rightSideSelection, _metrics);
            return leftSideAnalytics.Differentiate(rightSideAnalytics);
        }
    }
}
