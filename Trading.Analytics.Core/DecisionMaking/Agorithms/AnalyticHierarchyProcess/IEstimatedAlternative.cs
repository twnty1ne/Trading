using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess
{
    public interface IEstimatedAlternative<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISelection<TParameter, T> Alternative { get; }
        decimal Scores { get; }
    }
}
