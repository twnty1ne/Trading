using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core.SplitTesting
{
    public class EstimationParameter<T, R> : IEstimationParameter<T, R> where R : Enum
    {
        public EstimationParameter(IMetric<T, R> metric, Importance importance, EstimationWays way, decimal estimatableMinimum, decimal estimatableMaximum)
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
