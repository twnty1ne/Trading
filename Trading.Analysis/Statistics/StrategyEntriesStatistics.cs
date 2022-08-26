using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Trading.Analysis.Model;
using Trading.Analysis.Statistics.Results;

namespace Trading.Analysis.Statistics
{
    internal class StrategyEntriesStatistics : IStatistics<StrategiesEntriesResult>
    {
        private IEnumerable<IEntry> _entries;

        public StrategyEntriesStatistics(IEnumerable<IEntry> entries)
        {
            _entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        public StrategiesEntriesResult GetValue()
        {
            
            return new StrategiesEntriesResult
            {
                AmountOfEntries = _entries.Count(),
                AmountOfProfitEntries = _entries.Where(x => x.State == EntryState.HitTakeProfit).Count(),
                AmountOfLossEntries = _entries.Where(x => x.State == EntryState.HitStopLoss).Count(),
                AmountOfSkippedEntries = _entries.Where(x => x.State == EntryState.Skipped).Count(),
                Profit = CalculateProfitInPercents(),
                RiskReward = 2,
            };
        }

        private decimal CalculateProfitInPercents() 
        {
            return decimal.Zero;
        }
    }
}
