using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features.Attributes;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions.Features
{
    public abstract class Feature<TItem, T> : IFeature<TItem, T> where TItem : class
    {
        private readonly string _name;
        private readonly FeatureType _type;

        protected abstract IEnumerable<Type> AllowedPropertyTypes { get; }

        protected Feature(string name, FeatureType type)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _type = type;
            ValidateFeature();
        }

        public T GetValue(TItem item)
        {
            var feature = GetFeature();
            return Cast(feature.Property.GetValue(item));
        }

        private (PropertyInfo Property, FeatureAttribute Attribute) GetFeature()
        {
            var feature = typeof(TItem)
                .GetProperties()
                .Select(x =>(Property: x, Attributes : x.GetCustomAttributes(true).OfType<FeatureAttribute>()))
                .Select(x => (x.Property, Attribute : x.Attributes.FirstOrDefault(y => y.Name == _name)))
                .FirstOrDefault(x => x.Attribute != null);
            
            if (feature == default || feature.Attribute == default)
            {
                throw new ArgumentException($"there is no feature named : {_name}");
            }
            
            return feature;
        }

        private void ValidateFeature()
        {
            var feature = GetFeature();
            
            var valid = feature.Attribute.Type == _type 
                        && AllowedPropertyTypes.Contains(feature.Property.PropertyType);

            if (!valid)
                throw new ValidationException("feature is not valid");
        }

        protected abstract T Cast(object featureValue);
        
    }
}