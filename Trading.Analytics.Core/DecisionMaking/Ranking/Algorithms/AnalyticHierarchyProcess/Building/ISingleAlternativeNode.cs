using System;
using System.Collections.Generic;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building
{
    public interface ISingleAlternativeNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        IAlternativesNode<T, R, TParameter>  WithParameters(IEnumerable<IParameter<TParameter, decimal>> parameters);
        ISingleAlternativeNode<T, R, TParameter> HasAlternative(IReadOnlyCollection<T> selection);
        IFinalNode<T, R, TParameter> LastOne();
    }
}
