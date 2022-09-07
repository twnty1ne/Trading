using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analytics.Core.SplitTesting;
using Trading.Shared.Resolvers;

namespace Trading.Analytics.Core.Metrics
{
    internal class EstimationMetric<T, R> : IMetric<T, R> where R : Enum
    {
        private readonly IEstimationParameter<T, R> _parameter;
        private readonly decimal _weight;

        public EstimationMetric(IEstimationParameter<T, R> parameter, decimal weight)
        {
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _weight = weight;
        }

        public R Type => _parameter.Metric.Type;

        public IMetricResult<R> GetResult(ISelection<T> selection)
        {
            var innerResult = _parameter.Metric.GetResult(selection);
            var scores = GetNormalizedMetricResult(innerResult.Value);
            return new MetricResult<R>(innerResult.Type, scores * _weight);
        }

        private decimal GetNormalizedMetricResult(decimal value) 
        {
            //point range - 25
            //value - 50
            var pointRange = _parameter.EstimateableRangeMin / 100m;
            var scores = value / pointRange;
            if (_parameter.Way == EstimationWays.LowerTheBetter) return 100 - scores;
            return scores;
        }

        
    }
}
