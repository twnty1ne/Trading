using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess;

namespace Trading.Researching.Core.DecisionMaking.Agorithms
{
    interface IRanking<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        IEstimatedAlternative<T, R, TParameter> Best { get; }
        IEnumerable<IEstimatedAlternative<T, R, TParameter>> All { get; }
        IEstimatedAlternative<T, R, TParameter> Worst { get; }

    }
}
