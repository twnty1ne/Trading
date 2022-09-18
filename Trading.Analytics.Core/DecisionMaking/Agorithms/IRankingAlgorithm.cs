using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess;

namespace Trading.Researching.Core.DecisionMaking.Agorithms
{
    internal interface IRankingAlgorithm<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        public IRanking<T, R, TParameter> Rank();
    }
}
