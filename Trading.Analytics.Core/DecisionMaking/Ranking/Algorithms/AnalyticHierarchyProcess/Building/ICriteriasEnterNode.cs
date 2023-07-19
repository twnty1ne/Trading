using System;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface ICriteriasEnterNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISingleCriteriaNode<T, R, TParameter> HasCriteria(IMetric<T, R> metric, decimal estimatableMinimum, decimal estimatableMaximum);
    }
}
