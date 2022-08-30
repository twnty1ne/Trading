﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Analysis.Model;
using Trading.Analytics.Core;
using Trading.Analytics.Core.Metrics;

namespace Trading.Analysis.Analytics.Metrics
{
    internal class WinRateRatioMetric : IMetric<IEntry, StrategyMetrics>
    {
        private readonly IMetric<IEntry, StrategyMetrics> _metric;

        public WinRateRatioMetric()
        {
            _metric = CreateMetric();
        }

        public IMetricResult<StrategyMetrics> GetResult(IEnumerable<IEntry> selection)
        {
            return _metric.GetResult(selection);
        }

        private IMetric<IEntry, StrategyMetrics> CreateMetric() 
        {
            return new Metric<IEntry, StrategyMetrics>(CreateSelector(), StrategyMetrics.WinLossRatio);
        }

        private Func<IEnumerable<IEntry>, decimal> CreateSelector() 
        {
            return x =>
            {
                var amountOfWinEntries = Convert.ToDecimal(x.Where(x => x.State == EntryState.HitTakeProfit).Count());
                var amountOfLossEntries = Convert.ToDecimal(x.Where(x => x.State == EntryState.HitStopLoss).Count());
                if (amountOfWinEntries == 0) return decimal.Zero;
                if (amountOfLossEntries == 0) return 1m;
                return amountOfWinEntries / (amountOfLossEntries + amountOfWinEntries); 
            };
        }
        
    }
}
