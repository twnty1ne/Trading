using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class CriteriasEnterNode<T, R, TParameter> : ICriteriasEnterNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IContext<T, R, TParameter> _context;

        public CriteriasEnterNode(IContext<T, R, TParameter> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ISingleCriteriaNode<T, R, TParameter> HasCriteria(IMetric<T, R> metric, decimal estimatableMinimum, decimal estimatableMaximum)
        { 
            return new SingleCriteriaNode<T, R, TParameter>(_context, metric, estimatableMinimum, estimatableMaximum);
        }
    }
}
