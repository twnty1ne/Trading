using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class ImportanceNode<T, R, TParameter> : IImportanceNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly EstimationWays _estimationWay;
        private readonly IMetric<T, R> _metric;
        private readonly IContext<T, R, TParameter> _context;
        private readonly decimal _estimatableMaximum;
        private readonly decimal _estimatableMinimum;

        public ImportanceNode(EstimationWays estimationWay, IMetric<T, R> metric, IContext<T, R, TParameter> context, decimal estimatableMaximum, decimal estimatableMinimum)
        {
            _estimationWay = estimationWay;
            _metric = metric ?? throw new ArgumentNullException(nameof(metric));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _estimatableMaximum = estimatableMaximum;
            _estimatableMinimum = estimatableMinimum;
        }

        public ICriteriasNode<T, R, TParameter> WithImportanceLevel(Importance importance)
        {
            _context.AddCriteria(new Criteria<T, R>(_metric, importance, _estimationWay, _estimatableMinimum, _estimatableMaximum));
            return new CriteriasNode<T, R, TParameter>(_context);
        }
    }
}
