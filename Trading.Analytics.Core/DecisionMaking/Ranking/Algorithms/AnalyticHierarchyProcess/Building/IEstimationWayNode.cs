using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface IEstimationWayNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ICriteriasNode<T, R, TParameter> LowerTheBetter();
        ICriteriasNode<T, R, TParameter> HigherTheBetter();
    }
}
