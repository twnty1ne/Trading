using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analysis.Model
{
    internal enum EntryState
    {
        HitTakeProfit = 1,
        InProgress = 2,
        HitStopLoss = 3,
        Skiped = 4
    }
}
