using System;
using System.Collections.Generic;
using System.Text;
using Trading.Shared.Ranges;

namespace Trading.Analysis.Indicators
{
    public class FibonacciRange
    {
        public FibonacciRange(decimal level, IRange<decimal> priceRange)
        {
            Level = level;
            PriceRange = priceRange ?? throw new ArgumentNullException(nameof(priceRange));
        }

        public decimal Level { get; }
        public IRange<decimal> PriceRange { get; }
    }
}
