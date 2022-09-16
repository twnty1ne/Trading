using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface IEstimationWayNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ICriteriasNode<T, R, TParameter> LowerTheBetter();
        ICriteriasNode<T, R, TParameter> HigherTheBetter();
    }
}
