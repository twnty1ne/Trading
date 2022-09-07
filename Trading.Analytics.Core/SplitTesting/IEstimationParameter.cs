using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core.SplitTesting
{
    public interface IEstimationParameter<T, R> where R : Enum
    {
        Importance Importance { get; }
        decimal EstimateableRangeStartMax { get; }
        decimal EstimateableRangeMin { get; }
        public IMetric<T, R> Metric { get; }
        public EstimationWays Way { get; }
    }
}
