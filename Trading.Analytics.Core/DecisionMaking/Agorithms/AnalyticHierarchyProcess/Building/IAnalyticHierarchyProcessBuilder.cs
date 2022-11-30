using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface IAnalyticHierarchyProcessBuilder<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ICriteriasEnterNode<T, R, TParameter> Criterias();
    }
}
