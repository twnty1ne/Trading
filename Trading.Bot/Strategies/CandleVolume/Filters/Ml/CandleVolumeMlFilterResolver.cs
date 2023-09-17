using System;
using System.Collections.Generic;
using Trading.Bot.Strategies.CandleVolume.Filters.Ml.InsideChannelLong;
using Trading.Bot.Strategies.CandleVolume.Filters.Ml.InsideChannelShort;
using Trading.Bot.Strategies.CandleVolume.Filters.Ml.OutsideChannel;
using Trading.Bot.Strategies.Filters;
using Trading.MlClient;
using Trading.Shared.Resolvers;

namespace Trading.Bot.Strategies.CandleVolume.Filters.Ml;

public class CandleVolumeMlFilterResolver : IResolver<CandleVolumeSignalClassification, Filter<CandleVolumeStrategyContext>>
{
    private readonly IMlClient _mlClient;
    private readonly IResolver<CandleVolumeSignalClassification, Filter<CandleVolumeStrategyContext>> _resolver;

    public CandleVolumeMlFilterResolver(IMlClient mlClient)
    {
        _mlClient = mlClient ?? throw new ArgumentNullException(nameof(mlClient));
        _resolver =
            new Resolver<CandleVolumeSignalClassification, Filter<CandleVolumeStrategyContext>>(CreateDictionary());
    }

    public Filter<CandleVolumeStrategyContext> Resolve(CandleVolumeSignalClassification type)
    {
        return _resolver.Resolve(type);
    }

    public bool TryResolve(CandleVolumeSignalClassification type, out Filter<CandleVolumeStrategyContext> item)
    {
        return _resolver.TryResolve(type, out item);
    }

    private Dictionary<CandleVolumeSignalClassification, Func<Filter<CandleVolumeStrategyContext>>> CreateDictionary()
    {
        return new Dictionary<CandleVolumeSignalClassification, Func<Filter<CandleVolumeStrategyContext>>>
        {
            { CandleVolumeSignalClassification.OutsideChannel, () => new OutsideChannelMlFilter(_mlClient) },
            { CandleVolumeSignalClassification.InsideChannelLong, () => new InsideChannelLongMlFilter(_mlClient) },
            { CandleVolumeSignalClassification.InsideChannelShort, () => new InsideChannelShortMlFilter(_mlClient) }
        };
    }
}