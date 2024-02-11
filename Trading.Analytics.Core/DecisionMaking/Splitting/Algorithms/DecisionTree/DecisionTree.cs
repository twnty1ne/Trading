using System;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree
{
    public class DecisionTree<TItem, TMark> : IDecisionTree<TItem, TMark> 
        where TItem : class
        where TMark : Enum
    {
        private readonly QuestionNode<TItem, TMark> _questionNode;

        public DecisionTree(QuestionNode<TItem, TMark> questionNode)
        {
            _questionNode = questionNode ?? throw new ArgumentNullException(nameof(questionNode));
        }

        public TMark Decide(TItem item)
        {
            if (item is null)
                throw new ArgumentNullException();
            
            return _questionNode.Decide(item);
        }
    }
}