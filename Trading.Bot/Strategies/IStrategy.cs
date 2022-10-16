using System;

namespace Trading.Bot.Strategies
{
    public interface IStrategy
    {
        event EventHandler<ISignal> OnSignalFired;
    }
}
