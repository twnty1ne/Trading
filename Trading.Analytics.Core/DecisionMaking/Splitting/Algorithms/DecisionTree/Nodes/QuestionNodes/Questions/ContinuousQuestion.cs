using System;
using System.Collections.Generic;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Continuous;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions
{
    public class ContinuousQuestion<TItem> : IQuestion<TItem> where TItem : class
    {
        private readonly decimal _splitPoint;
        private readonly SplitSign _splitSign;
        private readonly Func<TItem, bool> _predicate;
        private readonly IFeature<TItem, decimal> _feature;
        
        public ContinuousQuestion(decimal splitPoint, SplitSign splitSign, string featureName)
        {
            _splitPoint = splitPoint;
            _splitSign = splitSign;
            _feature = new ContinuousFeature<TItem>(featureName);
            _predicate = CreatePredicate();
        }

        public SplitAnswer Ask(TItem item)
        {
            return _predicate.Invoke(item) ? SplitAnswer.Positive : SplitAnswer.Negative;
        }

        private Func<TItem, bool> CreatePredicate()
        {
            var predicateDictionary = new Dictionary<SplitSign, Func<TItem, bool>>
            {
                { SplitSign.Less, x => _feature.GetValue(x) < _splitPoint },
                { SplitSign.More, x => _feature.GetValue(x) > _splitPoint },
                { SplitSign.EqualsOrLess, x => _feature.GetValue(x) <= _splitPoint },
                { SplitSign.EqualsOrMore, x => _feature.GetValue(x) >= _splitPoint },
            };

            return predicateDictionary[_splitSign];
        }
    }
}