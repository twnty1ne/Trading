using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
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
