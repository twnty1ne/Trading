using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building;

namespace Trading.Researching.Core.DecisionMaking.Building
{
    public interface IDecisionMakingBuilder<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        IAnalyticHierarchyProcessBuilder<T, R, TParameter> AnalyticHierarchyProcess();
    }
}
