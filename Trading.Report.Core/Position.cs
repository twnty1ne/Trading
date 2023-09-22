using System;
using System.Collections.Generic;
using Trading.Core;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Report.Core
{
    public class Position : Entity
    {
        public DateTime EntryDate { get; set;  }
        public DateTime CloseDate { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal StopLoss { get; set; }
        public virtual Instrument Instrument { get; set; }
        public int InstrumentId { get; set; }
        public PositionStates State { get; set; }
        public PositionResult Result { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Leverage { get; set; }
        public decimal IMR { get; set; }
        public PositionSides Side { get; set; }
        public decimal Size { get; set; }
        public decimal InitialMargin { get; set; }
        public decimal UnrealizedPnL { get; set; }
        public decimal RealizedPnl { get; set; }
        public decimal ROE { get; set; }
        public long EntryDateTicks { get; set; }
        public string EntryDateStringValue { get; set; }
        public virtual ICollection<PositionPriceTick> Ticks { get; set; }
        public virtual ICollection<TakeProfit> TakeProfits { get; set; }
    }
}
