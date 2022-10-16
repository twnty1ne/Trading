using System;

namespace Trading.Bot.Strategies
{
    internal interface IInstrumentStrategyScope
    {
        event EventHandler<ISignal> OnSignalFired;
    }
}
