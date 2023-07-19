using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess
{
    public class Estimation<T, R, TParameter> : IEstimation<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        private readonly IEnumerable<ICriteria<T, R>> _criterias;

        public Estimation(IEnumerable<ICriteria<T, R>> criterias)
        {
            _criterias = criterias ?? throw new ArgumentNullException(nameof(criterias));
        }

        public IEstimatedAlternative<T, R, TParameter> Estimate(ISelection<TParameter, T> selection)
        {
            var data = _criterias.Select(x => _criterias.Select(y => Convert.ToDouble((object)x.Importance) / Convert.ToDouble((object)y.Importance)));
            var matrix = Matrix<double>.Build.DenseOfRows(data);
            var rows = matrix.EnumerateRows();
            var columns = matrix.EnumerateColumns();
            var columnSumVector = columns.Select(x => x.Divide(x.Sum()));
            var weightMatrix = Matrix<double>.Build.DenseOfColumnVectors(columnSumVector);
            var weights = weightMatrix.EnumerateRows().Select(x => x.Average());
            var weightedMetrics = _criterias.Zip(weights);
            var analytics = new Analytics<T, R>(selection, weightedMetrics.Select(x => new EstimationMetric<T, R>(x.First, Convert.ToDecimal(x.Second))).ToList());
            var results = analytics.GetResults();
            return new EstimatedAlternative<T, R, TParameter>(selection, results.Sum(x => x.Value));
        }
    }
}
