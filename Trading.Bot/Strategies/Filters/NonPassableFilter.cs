namespace Trading.Bot.Strategies.Filters;

public class NonPassableFilter<TContext> : Filter<TContext> where TContext : class
{
    public override bool Passes(TContext signal)
    {
        return false;
    }
}