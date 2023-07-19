using System;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree
{
    public class DecisionTree<TItem> : IDecisionTree<TItem>
    {
        private readonly QuestionNode<TItem> _questionNode;

        public DecisionTree(QuestionNode<TItem> questionNode)
        {
            _questionNode = questionNode ?? throw new ArgumentNullException(nameof(questionNode));
        }

        public Decision Decide(TItem item)
        {
            if (item is null)
                throw new ArgumentNullException();
            
            return _questionNode.Decide(item);
        }
    }
}