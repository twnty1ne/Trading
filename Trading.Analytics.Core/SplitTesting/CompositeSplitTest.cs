using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trading.Analytics.Core.SplitTesting
{
    public class CompositeSplitTest<T, R, TParameter> : ISplitTest<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {

        private readonly ISplitTest<T, R, TParameter> _leftNode;
        private readonly ISplitTest<T, R, TParameter> _rightNode;
        private readonly IReadOnlyCollection<IEstimationParameter<T, R>> _metrics;

        public CompositeSplitTest(IReadOnlyCollection<IEstimationParameter<T, R>> metrics, IReadOnlyCollection<ISelection<TParameter, T>> selections) 
        {
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
            _leftNode = Compose(metrics, selections).LeftNode;
            _rightNode = Compose(metrics, selections).RightNode;
        }


        public SplitTestResult<T, R, TParameter> GetDifference()
        {
            throw new Exception();
        }

        public ISelection<TParameter, T> GetOptimal()
        {
            var leftNodeResult = _leftNode.GetOptimal();
            var rightNodeResult = _rightNode.GetOptimal();
            return new SplitTest<T, R, TParameter>(_metrics, leftNodeResult, rightNodeResult).GetOptimal();
        }

        private (ISplitTest<T, R, TParameter> LeftNode, ISplitTest<T, R, TParameter> RightNode) Compose(IReadOnlyCollection<IEstimationParameter<T, R>> metrics, 
            IReadOnlyCollection<ISelection<TParameter, T>> selections) 
        {
            var halvedSelections = Halve(selections);
            var leftNode = halvedSelections.SecondHalf.Count() > 2 ? (ISplitTest<T, R, TParameter>)new CompositeSplitTest<T, R, TParameter>(metrics, halvedSelections.FirstHalf) 
                :  new SplitTest<T, R, TParameter>(_metrics, halvedSelections.FirstHalf.ElementAt(0), halvedSelections.FirstHalf.ElementAt(1));
            var rightNode = halvedSelections.SecondHalf.Count() > 2 ? (ISplitTest<T, R, TParameter>)new CompositeSplitTest<T, R, TParameter>(metrics, halvedSelections.SecondHalf)
               : new SplitTest<T, R, TParameter>(_metrics, halvedSelections.SecondHalf.ElementAt(0), halvedSelections.SecondHalf.ElementAt(1));
            return (leftNode, rightNode);
        }

        private (IReadOnlyCollection<ISelection<TParameter, T>> FirstHalf, IReadOnlyCollection<ISelection<TParameter, T>> SecondHalf) Halve(
            IReadOnlyCollection<ISelection<TParameter, T>>  selections) 
        {
            var half = selections.Count()  / 2;
            var values = selections.Take(half);
            var newCollection = selections.Skip(half);
            return (values.ToList(), newCollection.ToList());
        }
    }
}
