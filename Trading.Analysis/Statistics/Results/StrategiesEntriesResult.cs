using System;
using System.Collections.Generic;
using System.Text;
using Trading.Analysis.Model;

namespace Trading.Analysis.Statistics.Results
{
    public class StrategiesEntriesResult : IStatisticsResult
    {
        public IEnumerable<IEntry> Entries { get; set; }
        public int AmountOfEntries { get; set; }
        public int AmountOfProfitEntries { get; set; }
        public int AmountOfSkippedEntries { get; set; }
        public int AmountOfLossEntries { get; set; }
        public int RiskReward { get; set; }
        public decimal Profit { get; set; }
        public decimal RiskPerEntry { get; set; }

    }
}
