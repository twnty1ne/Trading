using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface IFinalNode<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        IDecisionMaking<T, R, TParameter> Build();
    }
}
