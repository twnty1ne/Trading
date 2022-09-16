using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analysis.Model;
using Trading.Researching.Core;
using Trading.Researching.Core.Analytics.Metrics;

namespace Trading.Analysis.Analytics.Metrics
{
    public class WinRateRatioMetric : IMetric<IEntry, StrategyMetrics>
    {
        private readonly IMetric<IEntry, StrategyMetrics> _metric;

        public WinRateRatioMetric()
        {
            _metric = CreateMetric();
        }

        public StrategyMetrics Type  { get => _metric.Type; }

        public IMetricResult<StrategyMetrics> GetResult(ISelection<IEntry> selection)
        {
            return _metric.GetResult(selection);
        }

        private IMetric<IEntry, StrategyMetrics> CreateMetric() 
        {
            return new Metric<IEntry, StrategyMetrics>(CreateSelector(), StrategyMetrics.WinLossRatio);
        }

        private Func<ISelection<IEntry>, decimal> CreateSelector() 
        {
            return x =>
            {
                var amountOfWinEntries = Convert.ToDecimal(x.Data.Where(x => x.State == EntryState.HitTakeProfit).Count());
                var amountOfLossEntries = Convert.ToDecimal(x.Data.Where(x => x.State == EntryState.HitStopLoss).Count());
                if (amountOfWinEntries == 0) return decimal.Zero;
                if (amountOfLossEntries == 0) return 1m;
                return amountOfWinEntries / (amountOfLossEntries + amountOfWinEntries); 
            };
        }
        
    }
}
