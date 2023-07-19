using System;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms;

namespace Trading.Researching.Core.DecisionMaking.Ranking
{
    internal class DecisionMaking<T, R, TParameter> : IDecisionMaking<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IRankingAlgorithm<T, R, TParameter> _algorithm;

        public DecisionMaking(IRankingAlgorithm<T, R, TParameter> algorithm)
        {
            _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        }

        public ISelection<TParameter, T> Decide()
        {
            return _algorithm.Rank().Best.Alternative;
        }
    }
}
