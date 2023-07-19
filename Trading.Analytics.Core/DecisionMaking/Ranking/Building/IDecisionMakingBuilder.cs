using System;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Building
{
    public interface IDecisionMakingBuilder<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        IAnalyticHierarchyProcessBuilder<T, R, TParameter> AnalyticHierarchyProcess();
    }
}
