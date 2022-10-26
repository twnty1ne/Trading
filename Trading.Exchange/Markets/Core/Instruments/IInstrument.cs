using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Core.Instruments
{
    public interface IInstrument
    {
        IInstrumentName Name { get; }
        ITimeframe GetTimeframe(Timeframes.Timeframes type);
        
    }
}
