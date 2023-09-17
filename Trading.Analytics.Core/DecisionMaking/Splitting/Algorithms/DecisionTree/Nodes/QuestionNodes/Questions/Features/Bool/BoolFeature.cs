using System;
using System.Collections.Generic;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Bool
{
    public class BoolFeature<TItem> : Feature<TItem, bool> where TItem : class
    {
        public BoolFeature(string name) : base(name, FeatureType.Bool)
        {
        }

        protected override IEnumerable<Type> AllowedPropertyTypes =>
            new List<Type> { typeof(bool) };

        protected override bool Cast(object featureValue)
        {
            return Convert.ToBoolean(featureValue);
        }
    }
}