using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface ICriteriasEnterNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISingleCriteriaNode<T, R, TParameter> HasCriteria(IMetric<T, R> metric, decimal estimatableMinimum, decimal estimatableMaximum);
    }
}
