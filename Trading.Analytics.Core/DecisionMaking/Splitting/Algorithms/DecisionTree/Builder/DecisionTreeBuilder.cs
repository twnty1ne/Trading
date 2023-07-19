﻿using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.QuestionNodes.Questions;

namespace Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Builder
{
    public class DecisionTreeBuilder<TItem> : IDecisionTreeBuilder<TItem> where TItem : class
    {
        public DecisionTree<TItem> FromXml(XElement document)
        {
            var xmlTree = document.Elements().ToList().First();
            var rootQuestion = BuildQuestionNode(xmlTree);
            return new DecisionTree<TItem>(rootQuestion);
        }

        private QuestionNode<TItem> BuildQuestionNode(XElement splitNodeXml)
        {
            var question = BuildQuestion(splitNodeXml.Elements().First(x => x.Name == "question"));
            var positiveNode = BuildAnswerNode(splitNodeXml.Elements().First(x => x.Name == "positiveNode"));
            var negativeNode = BuildAnswerNode(splitNodeXml.Elements().First(x => x.Name == "negativeNode"));
            return new QuestionNode<TItem>(positiveNode, negativeNode, question);
        }

        private INode<TItem> BuildAnswerNode(XElement element)
        {
            var nodeElement = element.Elements().First();
            
            return nodeElement.Name == "splitNode"
                ? BuildQuestionNode(nodeElement)
                : BuildDecisionNode(nodeElement);
        }

        private INode<TItem> BuildDecisionNode(XElement element)
        {
            var decision = element.Name == "positiveDecision" ? Decision.Positive : Decision.Negative;
            return new DecisionNode<TItem>(decision);
        }
        
        private IQuestion<TItem> BuildQuestion(XElement questionXml)
        {
            var stringFeatureType = questionXml.Attributes().First(x => x.Name == "feature_type").Value;
            var feature = questionXml.Attributes().First(x => x.Name == "feature").Value;
            var featureTypeParsed = Enum.TryParse<FeatureType>(stringFeatureType, true, out var featureType);
            
            if (!featureTypeParsed)
                throw new ArgumentOutOfRangeException($"{stringFeatureType} is not valid value for feature type");
            
            if (featureType == FeatureType.Bool)
            {
                return new BoolQuestion<TItem>(feature);
            }

            if (featureType == FeatureType.Continuous)
            {
                var ci = CultureInfo.InvariantCulture.Clone() as CultureInfo;
                ci.NumberFormat.NumberDecimalSeparator = ".";
                
                var splitPoint = decimal.Parse(questionXml.Attributes().First(x => x.Name == "split_point").Value, ci);
                
                var stringSplistSign = questionXml.Attributes().First(x => x.Name == "split_sign").Value;
                var signParsed = Enum.TryParse<SplitSign>(stringSplistSign, true, out var splitSign);

                if (!signParsed)
                    throw new ArgumentOutOfRangeException($"{stringSplistSign} is not valid value for split sign");
                
                return new ContinuousQuestion<TItem>(splitPoint, (SplitSign)splitSign, feature);
            }

            throw new NotSupportedException($"{stringFeatureType} is not supported");
        }
    }
}