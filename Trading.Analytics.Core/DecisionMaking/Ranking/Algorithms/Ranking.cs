using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms
{
    internal class Ranking<T, R, TParameter> : IRanking<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IEnumerable<IEstimatedAlternative<T, R, TParameter>> _alternatives;

        public Ranking(IEnumerable<IEstimatedAlternative<T, R, TParameter>> alternatives)
        {
            _ = alternatives ?? throw new ArgumentNullException(nameof(alternatives));
            _alternatives = alternatives.OrderByDescending(x => x.Scores);
        }

        public IEstimatedAlternative<T, R, TParameter> Best { get => _alternatives.First(); }

        public IEnumerable<IEstimatedAlternative<T, R, TParameter>> All { get => _alternatives; }

        public IEstimatedAlternative<T, R, TParameter> Worst { get => _alternatives.Last();}
    }
}
