using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analysis.Model
{
    public enum EntryState
    {
        HitTakeProfit = 1,
        InProgress = 2,
        HitStopLoss = 3,
        Skipped = 4
    }
}
