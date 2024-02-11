using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface IAnalyticHierarchyProcessBuilder<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ICriteriasEnterNode<T, R, TParameter> Criterias();
    }
}
