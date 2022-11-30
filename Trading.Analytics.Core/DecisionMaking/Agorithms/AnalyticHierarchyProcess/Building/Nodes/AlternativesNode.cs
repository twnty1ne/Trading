using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class AlternativesNode<T, R, TParameter> : IAlternativesNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IContext<T, R, TParameter> _context;

        public AlternativesNode(IContext<T, R, TParameter> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ISingleAlternativeNode<T, R, TParameter> HasAlternative(IReadOnlyCollection<T> selection)
        {
            return new SingleAlternativeNode<T, R, TParameter>(selection, _context);
        }

        public IFinalNode<T, R, TParameter> LastOne()
        {
            return new FinalNode<T, R, TParameter>(_context);
        }
    }
}
