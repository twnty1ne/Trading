using System;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Ranking
{
    internal class EstimationMetric<T, R> : IMetric<T, R> where R : Enum
    {
        private readonly ICriteria<T, R> _criteria;
        private readonly decimal _weight;

        public EstimationMetric(ICriteria<T, R> parameter, decimal weight)
        {
            _criteria = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _weight = weight;
        }

        public R Type => _criteria.Metric.Type;

        public IMetricResult<R> GetResult(ISelection<T> selection)
        {
            var innerResult = _criteria.Metric.GetResult(selection);
            var scores = GetNormalizedMetricResult(innerResult.Value);
            return new MetricResult<R>(innerResult.Type, scores * _weight);
        }

        private decimal GetNormalizedMetricResult(decimal value) 
        {
            var pointRange = _criteria.EstimatableMaximum / 100m;
            var scores = value / pointRange;
            if (_criteria.Way == EstimationWays.LowerTheBetter) return 100m - scores;
            return scores;
        }  
    }
}
