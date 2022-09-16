using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class AlternativesEntryNode<T, R, TParameter> : IAlternativesEntryNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IContext<T, R, TParameter> _context;

        public AlternativesEntryNode(IContext<T, R, TParameter> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ISingleAlternativeNode<T, R, TParameter> HasAlternative(IReadOnlyCollection<T> selection)
        {
            return new SingleAlternativeNode<T, R, TParameter>(selection, _context);
        }
    }
}
