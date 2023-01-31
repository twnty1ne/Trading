using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Report.Core
{
    public class Timeframe : Entity
    {
        public string Name { get; set; }
        public Timeframes Type { get; set; }
    }
}
