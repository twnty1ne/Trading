using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building
{
    internal class Context<T, R, TParameter> : IContext<T, R, TParameter> 
        where R : Enum
        where TParameter : Enum
    {
        private readonly List<ISelection<TParameter, T>> _alternatives = new List<ISelection<TParameter, T>>();
        private readonly List<ICriteria<T, R>> _criterias = new List<ICriteria<T, R>>();

        public IEnumerable<ISelection<TParameter, T>> Alternatives { get => _alternatives; }

        public IEnumerable<ICriteria<T, R>> Criterias { get => _criterias; }

        public void AddAlternative(ISelection<TParameter, T> alternative)
        {
            _alternatives.Add(alternative);
        }

        public void AddCriteria(ICriteria<T, R> criteria)
        {
            _criterias.Add(criteria);
        }
    }
}
