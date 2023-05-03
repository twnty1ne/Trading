using System;
using Trading.Core;

namespace Trading.Report.Core
{
    public class TradeCandle : Entity
    {
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal Volume { get; set; }
        
        public int TradeId { get; set; }
        public virtual Trade Trade { get; set; }
    }
}