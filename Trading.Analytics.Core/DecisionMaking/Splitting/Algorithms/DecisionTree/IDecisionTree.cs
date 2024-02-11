using System;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree
{
    public interface IDecisionTree<TItem, TMark>
        where TItem : class
        where TMark : Enum
    {
        TMark Decide(TItem item);
    }
}