using System;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes
{
    public class DecisionNode<TItem, TMark> : INode<TItem, TMark>
        where TItem : class
        where TMark : Enum
    {
        private readonly TMark _decision;

        internal DecisionNode(TMark decision)
        {
            _decision = decision;
        }

        public TMark Decide(TItem item)
        {
            return _decision;
        }
    }
}