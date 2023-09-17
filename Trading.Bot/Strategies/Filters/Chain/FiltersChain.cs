using System.Collections.Generic;

namespace Trading.Bot.Strategies.Filters.Chain;

public class FiltersChain<TContext> : IFilter<TContext> 
    where TContext : class
{
    private readonly LinkedList<IFilter<TContext>> _chain;

    public FiltersChain(IEnumerable<IFilter<TContext>> filters)
    {
        _chain = new LinkedList<IFilter<TContext>>(filters);
    }

    public bool Passes(TContext signal)
    {
        var passes = true;
        var currenFilter = _chain.First;

        while (passes && currenFilter is not null)
        {
            passes = currenFilter.Value.Passes(signal);
            currenFilter = currenFilter.Next;
        }

        return passes;
    }
}