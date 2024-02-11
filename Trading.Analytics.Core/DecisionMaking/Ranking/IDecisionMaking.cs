using System;

namespace Trading.Researching.Core.DecisionMaking.Ranking
{
    public interface IDecisionMaking<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISelection<TParameter, T> Decide();
    }
}
