using System;
using System.Collections.Generic;
using System.Text;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Bot.Sessions
{
    public interface ITrade
    {
        Timeframes Timeframe { get; }
        IPosition Position { get; }
        Strategies.Strategies Strategy { get; }
        ISignal Signal { get; }
    }
}
