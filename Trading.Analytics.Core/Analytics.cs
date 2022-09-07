﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analytics.Core
{
    public class Analytics<T, R> : IAnalytics<T, R> where R : Enum
    {
        private readonly ISelection<T> _selection;
        private readonly IReadOnlyCollection<IMetric<T, R>> _metrics;

        public Analytics(ISelection<T> selection, IReadOnlyCollection<IMetric<T, R>> metrics)
        {
            _selection = selection ?? throw new ArgumentNullException(nameof(selection));
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        }

        public IReadOnlyCollection<MetricDifference_T<R>> Differentiate(IAnalytics<T, R> anotherAnalitics)
        {
            var result1 = anotherAnalitics.GetResults();
            var result2 = GetResults();
            return result1.Select(x => new MetricDifference_T<R>(x, result2.FirstOrDefault(y => y.Type.Equals(x.Type)))).ToList().AsReadOnly();
        }

        public IEnumerable<IMetricResult<R>> GetResults()
        {
            return _metrics.Select(x => x.GetResult(_selection));
        }
    }
}
