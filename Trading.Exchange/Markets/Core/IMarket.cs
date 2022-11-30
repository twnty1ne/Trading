﻿using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Markets.Core
{
    public interface IMarket<T> where T : IInstrument
    {
        IBalance Balance { get; }
        T GetInstrument(IInstrumentName name);
    }

}
