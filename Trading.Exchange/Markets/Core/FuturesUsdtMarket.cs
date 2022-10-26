using Microsoft.Extensions.Caching.Memory;
using System;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Markets.Core
{
    internal class FuturesUsdtMarket : IMarket<IFuturesInstrument>
    {
        private readonly IMemoryCache _instruments;
        private readonly Func<IInstrumentName, IFuturesInstrument> _factory;

        public FuturesUsdtMarket(Func<IInstrumentName, IFuturesInstrument> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _instruments = new MemoryCache(new MemoryCacheOptions());
        }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return _instruments.GetOrCreate(name.GetFullName(), (x) => _factory.Invoke(name));
        }
    }
}
