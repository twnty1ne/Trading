using System;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes
{
    public class QuestionNode<TItem> : INode<TItem>
    {
        private readonly INode<TItem> _positiveNode;
        private readonly INode<TItem> _negativeNode;
        private readonly IQuestion<TItem> _question;

        public QuestionNode(INode<TItem> positiveNode, INode<TItem> negativeNode, IQuestion<TItem> question)
        {
            _positiveNode = positiveNode ?? throw new ArgumentNullException(nameof(positiveNode));
            _negativeNode = negativeNode ?? throw new ArgumentNullException(nameof(negativeNode));
            _question = question ?? throw new ArgumentNullException(nameof(question));
        }

        public Decision Decide(TItem item)
        {
            return _question.Ask(item) == SplitAnswer.Positive ? _positiveNode.Decide(item) : _negativeNode.Decide(item);
        }
    }
}