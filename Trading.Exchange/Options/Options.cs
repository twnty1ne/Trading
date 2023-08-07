using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;

namespace Trading.Exchange
{
    public class Options
    {
        public ConnectionEnum ConnectionType { get; set; }
        public RealtimeOptions RealtimeOptions { get; set; }
        public HistorySimulationOptions HistorySimulationOptions { get; set; }
    }
}
