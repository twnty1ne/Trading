using System;
using System.Collections.Generic;

namespace Trading.Bot.Strategies.Filters.Ml;

public interface IFeatureParser<TContext, TFeature>
    where TFeature : Enum
    where TContext : class
{
    IEnumerable<(TFeature Feature, decimal Value)> Parse(TContext signal);
}