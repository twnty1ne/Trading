using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analysis.Analytics
{
    public class EntrySelectionMetadata
    {
        public EntrySelectionMetadata(decimal riskReward, decimal stopLossTreshold)
        {
            RiskReward = riskReward;
            StopLossTreshold = stopLossTreshold;
        }

        public decimal RiskReward { get; private set; }
        public decimal StopLossTreshold { get; set; }
    }
}
