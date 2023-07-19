using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface IImportanceNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ICriteriasNode<T, R, TParameter> WithImportanceLevel(Importance importance);
    }
}
