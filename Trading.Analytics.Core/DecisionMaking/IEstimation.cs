using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess;

namespace Trading.Researching.Core.DecisionMaking
{
    public interface IEstimation<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        IEstimatedAlternative<T, R, TParameter> Estimate(ISelection<TParameter, T> selection);
    }
}
