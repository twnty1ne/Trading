using System;
using System.Collections.Generic;
using System.Text;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core.Instruments.Positions;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Bot.Sessions
{
    internal class Trade : ITrade
    {
        public Trade(IPosition position, ISignal signal)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            _ = signal ?? throw new ArgumentNullException(nameof(signal));
            Timeframe = signal.Timeframe;
            Strategy = signal.Strategy;
        }

        public Timeframes Timeframe { get; }

        public IPosition Position { get; }

        public Strategies.Strategies Strategy { get; }
    }
}
