using System;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Building
{
    public class DecisionMakingBuilder<T, R, TParameter> : IDecisionMakingBuilder<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        public IAnalyticHierarchyProcessBuilder<T, R, TParameter> AnalyticHierarchyProcess()
        {
            return new AnalyticHierarchyProcessBuilder<T, R, TParameter>();
        }
    }
}

