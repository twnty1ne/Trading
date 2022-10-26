using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Markets.Core
{
    public interface IMarket<T> where T : IInstrument
    {
        T GetInstrument(IInstrumentName name);
    }

}
