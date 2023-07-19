using System.Xml;
using System.Xml.Linq;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Builder
{
    public interface IDecisionTreeBuilder<TItem>
    {
        DecisionTree<TItem> FromXml(XElement document);
    }
}