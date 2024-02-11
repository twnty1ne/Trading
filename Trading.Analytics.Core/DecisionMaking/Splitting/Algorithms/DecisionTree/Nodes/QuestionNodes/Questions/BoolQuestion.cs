using System;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Bool;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions
{
    public class BoolQuestion<TItem> : IQuestion<TItem> where TItem : class
    {
        private readonly IFeature<TItem, bool> _feature;

        public BoolQuestion(string featureName)
        {
            _feature = new BoolFeature<TItem>(featureName);
        }

        public SplitAnswer Ask(TItem item)
        {
            return _feature.GetValue(item) ? SplitAnswer.Positive : SplitAnswer.Negative;
        }
    }
}