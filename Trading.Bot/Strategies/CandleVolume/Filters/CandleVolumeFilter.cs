using System;
using System.Collections.Generic;
using Trading.Bot.Strategies.CandleVolume.Filters.Ml;
using Trading.Bot.Strategies.Filters;
using Trading.Bot.Strategies.Filters.Chain;
using Trading.MlClient;

namespace Trading.Bot.Strategies.CandleVolume.Filters;

public class CandleVolumeFilter : Filter<CandleVolumeStrategyContext>
{
    private readonly IFilter<CandleVolumeStrategyContext> _filterChain;
    private readonly IMlClient _mlClient;
    private readonly IContextParser<CandleVolumeStrategyContext> _contextParser;

    public CandleVolumeFilter(IMlClient mlClient)
    {
        _mlClient = mlClient ?? throw new ArgumentNullException(nameof(mlClient));
        _contextParser = new CandleVolumeContextParser();
        _filterChain = new FiltersChain<CandleVolumeStrategyContext>(CreateFiltersList());
    }

    public override bool Passes(CandleVolumeStrategyContext signal)
    {
        return _filterChain.Passes(signal);
    }

    private IEnumerable<IFilter<CandleVolumeStrategyContext>> CreateFiltersList()
    {
        return new List<IFilter<CandleVolumeStrategyContext>>
        {
            new CandleVolumeOutlierFilter(),
            new CandleVolumeMlFilter(_mlClient)
        };
    }
}