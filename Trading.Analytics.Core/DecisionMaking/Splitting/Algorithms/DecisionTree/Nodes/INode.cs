using System;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes
{
    public interface INode<TItem, TMark>
        where TItem : class
        where TMark : Enum
    {
        TMark Decide(TItem item);
    }
}