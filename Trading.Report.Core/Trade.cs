using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.Report.Core
{
    public class Trade : Entity
    {
        public int TimeframeId { get; set; }  
        public Timeframe Timeframe { get; set; }
        public int StrategyId { get; set; }
        public Strategy Strategy { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
