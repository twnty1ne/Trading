using System;
using Trading.Bot.Strategies.CandleVolume.Filters;
using Trading.Bot.Strategies.Filters;
using Trading.MlClient;

namespace Trading.Bot.Strategies.CandleVolume;

public class CandleVolumeStrategyFilterAdapter : ISignalsFilter
{
    private readonly IContextParser<CandleVolumeStrategyContext> _contextParser;
    private readonly IFilter<CandleVolumeStrategyContext> _filter;


    public CandleVolumeStrategyFilterAdapter(IMlClient mlClient)
    {
        _contextParser = new CandleVolumeContextParser();
        _filter = new CandleVolumeFilter(mlClient);
    }

    public bool Passes(ISignal signal)
    {
        return _filter.Passes(_contextParser.Parse(signal));
    }
}