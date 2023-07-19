using System;
using System.Collections.Generic;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms
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
