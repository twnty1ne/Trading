using System;
using System.Collections.Generic;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Continuous
{
    public class ContinuousFeature<TItem> : Feature<TItem, decimal> where TItem : class
    {
        public ContinuousFeature(string name) : base(name, FeatureType.Continuous)
        {
        }

        protected override IEnumerable<Type> AllowedPropertyTypes => 
            new List<Type> { typeof(int), typeof(decimal) };

        protected override decimal Cast(object featureValue)
        {
            return (decimal)featureValue;
        }
    }
}