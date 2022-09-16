using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Trading.Researching.Core.Analytics;

namespace Trading.Researching.Core.DecisionMaking.Agorithms.AnalyticHierarchyProcess.Building.Nodes
{
    internal class SingleAlternativeNode<T, R, TParameter> : ISingleAlternativeNode<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IReadOnlyCollection<T> _selection;
        private readonly IContext<T, R, TParameter> _context;

        public SingleAlternativeNode(IReadOnlyCollection<T> selection, IContext<T, R, TParameter> context)
        {
            _selection = selection ?? throw new ArgumentNullException(nameof(selection));
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


        public IAlternativesNode<T, R, TParameter> WithParameters(IEnumerable<IParameter<TParameter, decimal>> parameters)
        {
            _context.AddAlternative(new Selection<TParameter, T>(parameters.ToList(), _selection));
            return new AlternativesNode<T, R, TParameter>(_context);
        }
    }
}
