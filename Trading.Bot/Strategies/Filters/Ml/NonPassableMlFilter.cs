using System;
using Trading.MlClient.Resources.Models;

namespace Trading.Bot.Strategies.Filters.Ml;

public class NonPassableMlFilter<TContext, TFeatures> : MlFilter<TContext, TFeatures> 
    where TContext : class where TFeatures : Enum
{
    protected NonPassableMlFilter(IModelResource<TFeatures> resource, IFeatureParser<TContext, TFeatures> featureParser) 
        : base(resource, featureParser)
    {
    }
}