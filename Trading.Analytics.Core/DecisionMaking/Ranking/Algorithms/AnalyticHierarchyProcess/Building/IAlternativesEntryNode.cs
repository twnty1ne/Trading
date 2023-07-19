using System;
using System.Collections.Generic;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface IAlternativesEntryNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISingleAlternativeNode<T, R, TParameter> HasAlternative(IReadOnlyCollection<T> selection);
    }
}
