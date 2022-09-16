using System;
using System.Collections.Generic;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    internal interface IContext<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        IEnumerable<ISelection<TParameter, T>> Alternatives { get; }
        IEnumerable<ICriteria<T, R>> Criterias { get; }

        void AddAlternative(ISelection<TParameter, T> alternative);
        void AddCriteria(ICriteria<T, R> criteria);
    }
}