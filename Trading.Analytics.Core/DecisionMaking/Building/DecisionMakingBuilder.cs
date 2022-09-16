using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building;

namespace Trading.Researching.Core.DecisionMaking.Building
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

