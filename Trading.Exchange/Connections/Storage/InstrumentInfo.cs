using System;
using System.Collections.Generic;
using System.Text;
using Ganss.Excel;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Connections.Storage
{
    internal class InstrumentInfo : IInstrumentInfo
    {
        public string Name { get; set; }
        public ConnectionEnum Connection { get; set; }
        
        [DataFormat("dd.MM.yyyy HH:mm:ss")]
        public DateTime FirstCandleDate { get; set; }
    }
}
