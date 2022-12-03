using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Instruments;

namespace Trading.Exchange.Markets
{
    internal class FuturesUsdtMarket : IMarket<IFuturesInstrument>
    {
        private readonly IConnection _connection;

        public FuturesUsdtMarket(IConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public IFuturesInstrument GetInstrument(IInstrumentName name)
        {
            return new FuturesInstrument(name, _connection);
        }
    }
}
