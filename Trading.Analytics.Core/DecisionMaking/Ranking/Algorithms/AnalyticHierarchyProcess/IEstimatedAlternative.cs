using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess
{
    public interface IEstimatedAlternative<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISelection<TParameter, T> Alternative { get; }
        decimal Scores { get; }
    }
}
