using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking
{
    public class Criteria<T, R> : ICriteria<T, R> where R : Enum
    {
        public Criteria(IMetric<T, R> metric, Importance importance, EstimationWays way, decimal estimatableMinimum, decimal estimatableMaximum)
        {
            Importance = importance;
            Metric = metric ?? throw new ArgumentNullException(nameof(metric));
            Way = way;
            EstimatableMinimum = estimatableMinimum;
            EstimatableMaximum = estimatableMaximum;
        }

        public Importance Importance { get; private set; }
        public IMetric<T, R> Metric { get; private set; }
        public EstimationWays Way { get; private set; }
        public decimal EstimatableMinimum { get; private set; }
        public decimal EstimatableMaximum { get; private set; }
    }
}
