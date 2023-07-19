namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes
{
    public class DecisionNode<TItem> : INode<TItem>
    {
        private readonly Decision _decision;

        internal DecisionNode(Decision decision)
        {
            _decision = decision;
        }

        public Decision Decide(TItem item)
        {
            return _decision;
        }
    }
}