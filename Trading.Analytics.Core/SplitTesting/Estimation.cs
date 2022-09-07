using System;
using System.Collections.Generic;
using System.Linq;
using Kaos.Combinatorics;
using MathNet.Numerics.LinearAlgebra;
using Trading.Analytics.Core.Metrics;
using Trading.Shared.Resolvers;

namespace Trading.Analytics.Core.SplitTesting
{
    public class Estimation<T, R> : IEstimation<T, R> where R : Enum
    {
        private readonly IEnumerable<IEstimationParameter<T, R>> _parameters;


        public Estimation(IEnumerable<IEstimationParameter<T, R>> parameters)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public decimal Estimate(ISelection<T> selection)
        {

            var data = _parameters.Select(x => _parameters.Select(y => Convert.ToDouble(x.Importance) / Convert.ToDouble(y.Importance))); 
            var matrix = Matrix<double>.Build.DenseOfRows(data);
            var di = matrix.Diagonal();
            var rows = matrix.EnumerateRows();
            var columns = matrix.EnumerateColumns();
            var columnSumVector = columns.Select(x => x.Divide(x.Sum()));
            var weightMatrix = Matrix<double>.Build.DenseOfColumnVectors(columnSumVector);
            var weights = weightMatrix.EnumerateRows().Select(x => x.Average());
            var weighetMetrics = _parameters.Zip(weights);
            var analytics = new Analytics<T, R>(selection, weighetMetrics.Select(x => new EstimationMetric<T, R>(x.First, Convert.ToDecimal(x.Second))).ToList());
            var results = analytics.GetResults();
            return results.Sum(x => x.Value);
        }
    }
}
