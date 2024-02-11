using System;
using System.Xml;
using System.Xml.Linq;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Builder
{
    public interface IDecisionTreeBuilder<TItem, TMark> 
        where TItem : class
        where TMark : Enum
    {
        DecisionTree<TItem, TMark> FromXml(XElement document);
    }
}