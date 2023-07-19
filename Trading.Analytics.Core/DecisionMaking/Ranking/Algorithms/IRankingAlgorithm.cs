using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms
{
    internal interface IRankingAlgorithm<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        public IRanking<T, R, TParameter> Rank();
    }
}
