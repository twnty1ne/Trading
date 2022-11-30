using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    public interface IFinalNode<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        IDecisionMaking<T, R, TParameter> Build();
    }
}
