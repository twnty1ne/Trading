using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Exchange.Markets
{
    internal class FuturesUsdtMarket : IMarket<IFuturesInstrument>
    {
        private readonly IConnection _connection;
        private readonly IMemoryCache _instruments;

        public FuturesUsdtMarket(IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _instruments = new MemoryCache(new MemoryCacheOptions());
        }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return _instruments.GetOrCreate(name.GetFullName(), (x) => new FuturesInstrument(name, _connection));
        }
    }
}
