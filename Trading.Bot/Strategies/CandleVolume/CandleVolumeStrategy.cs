﻿using System;
using Trading.Exchange.Markets.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.MlClient;

namespace Trading.Bot.Strategies.CandleVolume
{
    public class CandleVolumeStrategy : IStrategy
    {
        private readonly IStrategy _strategy;

        public CandleVolumeStrategy(IMarket<IFuturesInstrument> market, IMlClient mlClient)
        {
            _strategy = new Strategy(new CandleVolumeAbstractFactory(mlClient), market);
            _strategy.OnSignalFired += HandleSignalFired;
        }

        public event EventHandler<ISignal> OnSignalFired;

        private void HandleSignalFired(object sender, ISignal signal) 
        {
            OnSignalFired?.Invoke(this, signal);
        }
    }
}
