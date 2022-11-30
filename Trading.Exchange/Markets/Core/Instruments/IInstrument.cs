using System;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Core.Instruments
{
    public interface IInstrument
    {
        event EventHandler<IPriceTick> OnPriceUpdated;
        IInstrumentName Name { get; }
        decimal Price { get; }
        ITimeframe GetTimeframe(Timeframes.Timeframes type);
        
    }
}
