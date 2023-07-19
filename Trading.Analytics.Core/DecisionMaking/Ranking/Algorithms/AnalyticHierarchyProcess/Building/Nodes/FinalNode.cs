using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class FinalNode<T, R, TParameter> : IFinalNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private IContext<T, R, TParameter> _context;

        public FinalNode(IContext<T, R, TParameter> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IDecisionMaking<T, R, TParameter> Build()
        {
            return new DecisionMaking<T, R, TParameter>(new AnalyticHierarchyProcessAlgorithm<T, R, TParameter>(_context));
        }
    }
}
