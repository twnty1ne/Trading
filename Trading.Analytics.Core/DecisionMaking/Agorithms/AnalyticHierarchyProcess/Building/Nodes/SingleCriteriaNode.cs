using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class SingleCriteriaNode<T, R, TParameter> : ISingleCriteriaNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IContext<T, R, TParameter> _context;
        private readonly IMetric<T, R> _metric;
        private readonly decimal _estimatableMaximum;
        private readonly decimal _estimatableMinimum;

        public SingleCriteriaNode(IContext<T, R, TParameter> context, IMetric<T, R> metric, decimal estimatableMinimum, decimal estimatableMaximum)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _metric = metric ?? throw new ArgumentNullException(nameof(metric));
            _estimatableMaximum = estimatableMaximum;
            _estimatableMinimum = estimatableMinimum;
        }

        public IImportanceNode<T, R, TParameter> LowerTheBetter()
        {
            return new ImportanceNode<T, R, TParameter>(EstimationWays.LowerTheBetter, _metric, _context, _estimatableMaximum, _estimatableMinimum);
        }

        public IImportanceNode<T, R, TParameter> HigherTheBetter()
        {
            return new ImportanceNode<T, R, TParameter>(EstimationWays.HigherTheBetter, _metric, _context, _estimatableMaximum, _estimatableMinimum);
        }

        public IEstimationWayNode<T, R, TParameter> WithImportanceLevel(Importance importance)
        {
            return new EstimationWayNode<T, R, TParameter>(importance, _metric, _context, _estimatableMaximum, _estimatableMinimum);
        }
    }
}
