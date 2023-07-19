using System;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class CriteriasNode<T, R, TParameter> : ICriteriasNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IContext<T, R, TParameter> _context;

        public CriteriasNode(IContext<T, R, TParameter> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAlternativesEntryNode<T, R, TParameter> Alternatives()
        {
            return new AlternativesEntryNode<T, R, TParameter>(_context);
        }

        public ISingleCriteriaNode<T, R, TParameter> HasCriteria(IMetric<T, R> metric, decimal estimatableMinimum, decimal estimatableMaximum)
        {
            return new SingleCriteriaNode<T, R, TParameter>(_context, metric, estimatableMinimum, estimatableMaximum);
        }
    }
}
