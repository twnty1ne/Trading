using System;
using System.Linq;
using Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess.Building;

namespace Trading.Researching.Core.DecisionMaking.Ranking.Algorithms.AnalyticHierarchyProcess
{
    internal class AnalyticHierarchyProcessAlgorithm<T, R, TParameter> : IRankingAlgorithm<T, R, TParameter>
        where R : Enum
        where TParameter : Enum
    {
        private readonly IContext<T, R, TParameter> _context;

        public AnalyticHierarchyProcessAlgorithm(IContext<T, R, TParameter> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRanking<T, R, TParameter> Rank()
        {
            var estimation = new Estimation<T, R, TParameter>(_context.Criterias);
            var test = _context.Alternatives.Select(x => estimation.Estimate(x)).ToList();
            return new Ranking<T, R, TParameter>(_context.Alternatives.Select(x => estimation.Estimate(x)).ToList());
        }
    }
}
