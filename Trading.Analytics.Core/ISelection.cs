using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core
{
    public interface ISelection<TParameter, TSelection> : ISelection<TSelection> where TParameter : Enum
    {
        IReadOnlyCollection<IParameter<TParameter, decimal>> Parameters { get; }
    }

    public interface ISelection<TSelection>
    {
        IReadOnlyCollection<TSelection> Data { get; }
    }
}
