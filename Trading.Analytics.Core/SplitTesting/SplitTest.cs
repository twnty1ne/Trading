using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analytics.Core.SplitTesting
{
    public class SplitTest<T, R, TParameter> : ISplitTest<T, R, TParameter>
        where R : Enum 
        where TParameter : Enum
    {
        private readonly IReadOnlyCollection<IEstimationParameter<T, R>> _metrics;
        private readonly ISelection<TParameter, T> _leftSideSelection;
        private readonly ISelection<TParameter, T> _rightSideSelection;

        public SplitTest(IReadOnlyCollection<IEstimationParameter<T, R>> metrics, ISelection<TParameter, T> leftSideSelection, ISelection<TParameter, T> rightSideSelection)
        {
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
            _leftSideSelection = leftSideSelection ?? throw new ArgumentNullException(nameof(leftSideSelection));
            _rightSideSelection = rightSideSelection ?? throw new ArgumentNullException(nameof(rightSideSelection));
        }


        public SplitTestResult<T, R, TParameter> GetDifference() 
        {
            //var leftSideAnalytics = new Analytics<T, R>(_leftSideSelection, _metrics);
            //var rightSideAnalytics = new Analytics<T, R>(_rightSideSelection, _metrics);
            //return leftSideAnalytics.Differentiate(rightSideAnalytics);
            var analytics = new Analytics<T, R>(_leftSideSelection, _metrics.Select(x => new MetricDifference<T, R>(_rightSideSelection, x.Metric)).ToList());
            return new SplitTestResult<T, R, TParameter>(_leftSideSelection, _rightSideSelection, analytics.GetResults().ToList());
        }

        public ISelection<TParameter, T> GetOptimal()
        {
            var estimation = new Estimation<T, R>(_metrics);
            var leftsideEstimationPoints = estimation.Estimate(_leftSideSelection);
            var rightSideEstimationPoints = estimation.Estimate(_rightSideSelection);
            if (leftsideEstimationPoints >= rightSideEstimationPoints) return _leftSideSelection;
            return _rightSideSelection;
        }

    }
}
