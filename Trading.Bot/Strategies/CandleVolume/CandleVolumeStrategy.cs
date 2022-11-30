using System;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Bot.Strategies.CandleVolume
{
    public class CandleVolumeStrategy : IStrategy
    {
        private readonly IStrategy _strategy;

        public CandleVolumeStrategy(IMarket<IFuturesInstrument> market)
        {
            _strategy = new Strategy(new CandleVolumeAbstractFactory(), market);
            _strategy.OnSignalFired += HandleSignalFired;
        }

        public event EventHandler<ISignal> OnSignalFired;

        private void HandleSignalFired(object sender, ISignal signal) 
        {
            OnSignalFired?.Invoke(this, signal);
        }
    }
}
