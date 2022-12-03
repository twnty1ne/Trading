using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Exchange.Markets
{
    public interface IMarket<T> where T : IInstrument
    {
        T GetInstrument(IInstrumentName name);
    }

    public interface IMarket
    {
        IMarket<IFuturesInstrument> FuturesUsdt { get; }
    }
}
