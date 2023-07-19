using System;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Attributes
{
    public class FeatureAttribute : Attribute
    {
        public FeatureAttribute(string name, FeatureType type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
        }

        public string Name { get; }
        public FeatureType Type { get; }
    }
}