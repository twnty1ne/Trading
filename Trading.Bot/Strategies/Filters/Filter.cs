namespace Trading.Bot.Strategies.Filters;

public abstract class Filter<TContext> : IFilter<TContext> 
    where TContext : class
{
    public abstract bool Passes(TContext signal);
    
}