namespace Trading.Bot.Strategies.Filters;

public interface IFilter<in TContext> where TContext : class
{
    bool Passes(TContext signal);
}


public interface IFilter
{
    bool Passes(ISignal signal);
}