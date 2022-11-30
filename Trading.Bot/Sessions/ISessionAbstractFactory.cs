using System;
using Trading.Bot.Strategies;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Bot.Sessions
{
    interface ISessionAbstractFactory
    {
        IStrategy Strategy { get; }
        IMarket<IFuturesInstrument> Market { get; }
        Action<ISignal> SignalFiredHandler { get; }
    }
}
