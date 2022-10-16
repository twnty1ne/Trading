﻿using System;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Bot.Sessions
{
    interface ISessionAbstractFactory
    {
        IStrategy Strategy { get; }
        IMarket<IFuturesInstrument> Market { get; }
        Action<ISignal> SignalFiredHandler { get; }
    }
}
