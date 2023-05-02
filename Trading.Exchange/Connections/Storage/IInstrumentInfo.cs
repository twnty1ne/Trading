using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Connections.Storage
{
    internal interface IInstrumentInfo
    {
        string Name { get; }
        ConnectionEnum Connection { get; }
        DateTime FirstCandleDate { get; }
    }
}
