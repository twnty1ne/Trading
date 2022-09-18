using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking
{
    public interface IDecisionMaking<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        ISelection<TParameter, T> Decide();
    }
}
