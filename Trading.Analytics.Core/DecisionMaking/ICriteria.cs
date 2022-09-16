using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking
{
    public interface ICriteria<T, R> where R : Enum
    {
        Importance Importance { get; }
        decimal EstimatableMinimum { get; }
        decimal EstimatableMaximum { get; }
        public IMetric<T, R> Metric { get; }
        public EstimationWays Way { get; }
    }
}
