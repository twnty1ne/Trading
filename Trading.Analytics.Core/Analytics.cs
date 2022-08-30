using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trading.Analytics.Core
{
    public class Analytics<T, R> : IAnalytics<T, R> where R : Enum
    {
        private readonly IReadOnlyCollection<T> _selection;
        private readonly IReadOnlyCollection<IMetric<T, R>> _metrics;

        public Analytics(IReadOnlyCollection<T> selection, IReadOnlyCollection<IMetric<T, R>> metrics)
        {
            _selection = selection ?? throw new ArgumentNullException(nameof(selection));
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        }

        public IEnumerable<IMetricResult<R>> GetResults()
        {
            return _metrics.Select(x => x.GetResult(_selection));
        }
    }
}
