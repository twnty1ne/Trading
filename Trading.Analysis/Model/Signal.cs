using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analysis.Model
{
    public class Signal
    {
        public string InstrumentName { get; set; }
        public Position Position { get; set; }
        public DateTime FiredDate { get; set; }
    }
}
