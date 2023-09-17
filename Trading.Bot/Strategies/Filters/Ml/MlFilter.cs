using System;
using Trading.MlClient.Resources.Models;

namespace Trading.Bot.Strategies.Filters.Ml;

public class MlFilter<TContext, TFeatures> : Filter<TContext>  
    where TFeatures : Enum 
    where TContext : class
{
    private readonly IModelResource<TFeatures> _resource;
    private readonly IFeatureParser<TContext, TFeatures> _featureParser;

    protected MlFilter(IModelResource<TFeatures> resource, IFeatureParser<TContext, TFeatures> featureParser)
    {
        _resource = resource;
        _featureParser = featureParser;
    }
    
    public override bool Passes(TContext signal)
    {
        return _resource
            .PredictAsync(_featureParser.Parse(signal))
            .GetAwaiter()
            .GetResult();
    }
}