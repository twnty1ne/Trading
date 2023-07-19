using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess
{
    internal class EstimatedAlternative<T, R, TParameter> : IEstimatedAlternative<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        public EstimatedAlternative(ISelection<TParameter, T> alternative, decimal scores)
        {
            Alternative = alternative ?? throw new ArgumentNullException(nameof(alternative));
            Scores = scores;
        }

        public ISelection<TParameter, T> Alternative { get; private set; }

        public decimal Scores { get; private set; }
    }
}
