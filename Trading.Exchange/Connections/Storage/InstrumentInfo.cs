using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Connections.Storage
{
    internal class InstrumentInfo : IInstrumentInfo
    {
        public string Name { get; set; }
        public ConnectionEnum Connection { get; set; }
        public DateTime FirstCandleDate { get; set; }
    }
}
