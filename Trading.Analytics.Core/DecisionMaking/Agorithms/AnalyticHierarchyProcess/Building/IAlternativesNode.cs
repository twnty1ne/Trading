using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface IAlternativesNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISingleAlternativeNode<T, R, TParameter> HasAlternative(IReadOnlyCollection<T> selection);

        IFinalNode<T, R, TParameter> LastOne();
    }
}
