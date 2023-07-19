using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes
{
    public interface INode<TItem>
    {
        Decision Decide(TItem item);
    }
}