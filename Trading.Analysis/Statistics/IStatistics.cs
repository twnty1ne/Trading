using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analysis.Statistics.Results;

namespace Trading.Analysis.Statistics
{
    public interface IStatistics<TResult> where TResult : IStatisticsResult
    {
        TResult GetValue();
    }
}
