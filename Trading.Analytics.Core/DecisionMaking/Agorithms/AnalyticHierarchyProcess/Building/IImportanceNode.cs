using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface IImportanceNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ICriteriasNode<T, R, TParameter> WithImportanceLevel(Importance importance);
    }
}
