using System;
using System.IO;
using System.Xml.Linq;
using Trading.Bot.Strategies.Filters;
using Trading.MlClient;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Builder;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml;

public class CandleVolumeMlFilter : Filter<CandleVolumeStrategyContext>
{
    private readonly IDecisionTree<CandleVolumeStrategyContext, CandleVolumeSignalClassification> _signalClassifier;
    private readonly IResolver<CandleVolumeSignalClassification, Filter<CandleVolumeStrategyContext>> _resolver;

    public CandleVolumeMlFilter(IMlClient mlClient)
    {
        _ = mlClient ?? throw new ArgumentNullException(nameof(mlClient));
        _signalClassifier = CreateDecisionTree();
        _resolver = new CandleVolumeMlFilterResolver(mlClient);
    }

    public override bool Passes(CandleVolumeStrategyContext signal)
    {
        var signalClassification = _signalClassifier.Decide(signal);
        
        return _resolver
            .Resolve(signalClassification)
            .Passes(signal);
    }

    private IDecisionTree<CandleVolumeStrategyContext, CandleVolumeSignalClassification> CreateDecisionTree()
    {
        var builder = new DecisionTreeBuilder<CandleVolumeStrategyContext, CandleVolumeSignalClassification>();
        var docPath = Path.Combine(Directory.GetCurrentDirectory(), "Strategies", "CandleVolume", 
            "Filters", "Ml", "CandleVolumeDecisionTree.xml");
        var doc = XElement.Load(docPath);
        var tree = builder.FromXml(doc);
        return tree;
    }
}