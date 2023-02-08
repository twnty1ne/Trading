using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Shared.Ranges;

namespace Trading.Analysis.Indicators
{
    public class FibonacciGrid : IEnumerable<FibonacciRange>
    {
        private readonly IRange<decimal> _priceRange;
        private readonly IEnumerable<decimal> _levels;
        private readonly IEnumerable<FibonacciRange> _ranges;

        public FibonacciGrid(IRange<decimal> priceRange)
        { 
            _levels = new List<decimal> { 0m, 0.236m, 0.382m, 0.5m, 0.618m, 0.786m, 1m };
            _ranges = CreateRanges();
            _priceRange = priceRange;
        }

        private IEnumerable<FibonacciRange> CreateRanges() 
        {
            var diff = _priceRange.To - _priceRange.From;
            var result = new List<FibonacciRange>();
            var lastValue = 0m;
            foreach (var value in _levels.Except(new List<decimal> { lastValue })) 
            {
                result.Add(new FibonacciRange(value, new Range<decimal>(diff * lastValue + _priceRange.From, diff * value + _priceRange.From)));
            }
            return result;
        }

        public IEnumerator<FibonacciRange> GetEnumerator()
        {
            return _ranges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ranges.GetEnumerator();
        }
    }
}
