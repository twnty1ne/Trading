using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analysis.Model
{
    public interface IEntry
    {
        public decimal TakeProfit { get; }
        public int Index { get; }
        public decimal Price { get; }
        public decimal StopLoss { get; }
        public EntryState State { get; }
        public DateTimeOffset Date { get; }
        public Position Position { get; }
    }
}
