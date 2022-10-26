using Trading.Exchange.Markets;

namespace Trading.Exchange
{
    public interface IExchange
    {
        public IMarket Market { get; }
    }
}
