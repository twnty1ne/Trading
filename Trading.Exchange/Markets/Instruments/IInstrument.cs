using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Candles;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Instruments
{
    public interface IInstrument
    {
        IInstrumentName Name { get; }
        ITimeframe GetTimeframe(Timeframes.Timeframes type);
        
    }
}
