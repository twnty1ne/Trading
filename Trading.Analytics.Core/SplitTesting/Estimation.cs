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

            var data = _parameters.Select(x => 
            {
                return _parameters
                .Select(y => Convert.ToDecimal(x.Importance) / Convert.ToDecimal(y.Importance))
                .Select(x => Convert.ToDouble(x));
            }); 
            var kvps = _parameters.Select(x  => Convert.ToDouble(x.Importance));
            //var data = new List<List<double>> 
            //{
            //    new List<double> { 1d, 3d, 7d},
            //    new List<double> { 0.333d, 1d, 2d },
            //    new List<double> { 0.143d, 0.500d, 1d}

            //};
            var matrix = Matrix<double>.Build.DenseOfRows(data);
            var di = matrix.Diagonal();
            var rows = matrix.EnumerateRows();
            var columns = matrix.EnumerateColumns();
            var columnSumVector = columns.Select(x => x.Divide(x.Sum()));
            var weightMatrix = Matrix<double>.Build.DenseOfColumnVectors(columnSumVector);
            var rows2 = weightMatrix.EnumerateRows().Select(x => x.Average());
            var weighetMetrics = _parameters.Zip(rows2);
            var test = rows2.Sum();
            //var metricRatioKvps = kvps.Select(x => new KeyValuePair<R, IEnumerable<decimal>>(x.Key, kvps.Select(y => Convert.ToDecimal(y.Value) / Convert.ToDecimal(x.Value))));
            //var metricWeights1 = metricRatioKvps.Select(x => new KeyValuePair<R, IEnumerable<decimal>>(x.Key, x.Value.Select(y => y / x.Value.Sum())));
            //var row = 0;
            //var metricWeights = metricWeights1.Select(x =>
            //{
            //    var value = new KeyValuePair<R, IEnumerable<decimal>>(x.Key, metricWeights1.Select(x => x.Value.ElementAt(row)).ToList());
            //    row++;
            //    return value;
            //}).Select(x => new KeyValuePair<R, decimal>(x.Key, x.Value.Average())).ToList();
            //var dictionary = new Dictionary<R, Func<decimal>>(metricWeights.Select(x => new KeyValuePair<R, Func<decimal>>(x.Key, () => x.Value)));
            //var resolver = new Resolver<R, decimal>(dictionary);
            var analytics = new Analytics<T, R>(selection, weighetMetrics.Select(x => new EstimationMetric<T, R>(x.First, Convert.ToDecimal(x.Second))).ToList());
            var results = analytics.GetResults();
            return results.Sum(x => x.Value);
        }

        private decimal CalculateNormalizedSum(IEnumerable<decimal> values)
        {
            return decimal.Zero;
        }

        private Dictionary<R, Func<decimal>> CreateResolvingDictionary()
        {
            var list = new List<Importance> { Importance.ExtraHigh, Importance.High };
            var result = list.Zip(list.Skip(1), (a, b) => Tuple.Create(a, b));
            var grouped = _parameters.GroupBy(x => x.Importance);
            var uniqueImportances = grouped.Select(x => x.Key);
            return new Dictionary<R, Func<decimal>>();
        }
    }
}
