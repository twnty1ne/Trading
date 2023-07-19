using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree
{
    public interface IDecisionTree<TItem>
    {
        Decision Decide(TItem item);
    }
}