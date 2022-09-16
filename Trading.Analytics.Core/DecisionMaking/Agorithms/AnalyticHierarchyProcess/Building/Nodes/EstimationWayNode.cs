using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class EstimationWayNode<T, R, TParameter> : IEstimationWayNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {

        private readonly Importance _importance;
        private readonly IMetric<T, R> _metric;
        private readonly IContext<T, R, TParameter> _context;
        private readonly decimal _estimatableMaximum;
        private readonly decimal _estimatableMinimum;

        public EstimationWayNode(Importance importance, IMetric<T, R> metric, IContext<T, R, TParameter> context, decimal estimatableMaximum, decimal estimatableMinimum)
        {
            _importance = importance;
            _metric = metric ?? throw new ArgumentNullException(nameof(metric));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _estimatableMaximum = estimatableMaximum;
            _estimatableMinimum = estimatableMinimum;
        }

        public ICriteriasNode<T, R, TParameter> HigherTheBetter()
        {
            _context.AddCriteria(new Criteria<T, R>(_metric, _importance, EstimationWays.HigherTheBetter, _estimatableMinimum, _estimatableMaximum));
            return new CriteriasNode<T, R, TParameter>(_context);
        }

        public ICriteriasNode<T, R, TParameter> LowerTheBetter()
        {
            _context.AddCriteria(new Criteria<T, R>(_metric, _importance, EstimationWays.LowerTheBetter, _estimatableMinimum, _estimatableMaximum));
            return new CriteriasNode<T, R, TParameter>(_context);
        }
    }
}
