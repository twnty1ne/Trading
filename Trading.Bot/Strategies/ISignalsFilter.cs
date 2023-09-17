namespace Trading.Bot.Strategies;

public interface ISignalsFilter
{
    bool Passes(ISignal signal);
}