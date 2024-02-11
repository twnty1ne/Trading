using System;
using Trading.Core;

namespace Trading.Report.Core
{
    public class PositionPriceTick : Entity
    {
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
    }
}