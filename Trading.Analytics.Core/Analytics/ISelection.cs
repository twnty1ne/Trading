using System;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.DecisionMaking.Ranking;

namespace Trading.Researching.Core
{
    public interface ISelection<TSelection>
    {
        IReadOnlyCollection<TSelection> Data { get; }
    }

    public interface ISelection<TParameter, TSelection> : ISelection<TSelection> where TParameter : Enum
    {
        IReadOnlyCollection<IParameter<TParameter, decimal>> Parameters { get; }
    }
}
