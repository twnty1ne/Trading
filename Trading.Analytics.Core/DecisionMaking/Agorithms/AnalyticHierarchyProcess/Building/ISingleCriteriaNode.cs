using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface ISingleCriteriaNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        IEstimationWayNode<T, R, TParameter> WithImportanceLevel(Importance importance);
        IImportanceNode<T, R, TParameter> LowerTheBetter();
        IImportanceNode<T, R, TParameter> HigherTheBetter();
    }
}
