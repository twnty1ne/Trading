using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess
{
    internal class AnalyticHierarchyProcessBuilder<T, R, TParameter> : IAnalyticHierarchyProcessBuilder<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        public ICriteriasEnterNode<T, R, TParameter> Criterias()
        {
            return new CriteriasEnterNode<T, R, TParameter>(new Context<T, R, TParameter>());
        }
    }
}
