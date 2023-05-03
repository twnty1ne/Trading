using System.Collections.Generic;
using Trading.Core;

namespace Trading.Report.Core
{
    public class Trade : Entity
    {
        public int TimeframeId { get; set; }  
        public virtual Timeframe Timeframe { get; set; }
        public int StrategyId { get; set; }
        public virtual Strategy Strategy { get; set; }
        
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        public virtual ICollection<TradeCandle> Candles { get; set; }
    }
}
