using System;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess;

namespace Trading.Researching.Core.DecisionMaking.Ranking
{
    public interface IEstimation<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        IEstimatedAlternative<T, R, TParameter> Estimate(ISelection<TParameter, T> selection);
    }
}
