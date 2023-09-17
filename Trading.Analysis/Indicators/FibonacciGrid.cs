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
            _priceRange = priceRange;
            _levels = new List<decimal> { 0m, 0.236m, 0.382m, 0.5m, 0.618m, 0.786m, 1m };
            _ranges = CreateRanges();
        }

        private IEnumerable<FibonacciRange> CreateRanges() 
        {
            var diff = _priceRange.To - _priceRange.From;
            var result = new List<FibonacciRange>();
            var lastValue = 0m;
            
            foreach (var value in _levels.Except(new List<decimal> { lastValue })) 
            {
                result.Add(new FibonacciRange(value, new Range<decimal>(diff * lastValue + _priceRange.From, diff * value + _priceRange.From)));
                lastValue = value;
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

        public decimal GetLevel(decimal price)
        {
            var range = _ranges.FirstOrDefault(x => x.PriceRange.Contains(price));

            if (range is null)
            {
                var max = _ranges.Max(x => x.PriceRange.To);
                return price > max ? 3 : -2;
            }

            return range.Level;
        }
        
        public decimal GetEquilibriumDistance(decimal price)
        {
            var eq = _ranges.First(x => x.Level == 0.5m).PriceRange.From;
            var topBorder = _ranges.Max(x => x.PriceRange.To);
            
            var topEqDiff = topBorder - eq;
            var priceEqDiff = Math.Abs(price - eq);

            var sign = price > eq ? 1 : -1;
            var distance =  priceEqDiff / topEqDiff * sign;

            return distance;
        }

        public decimal Size()
        {
            var topBorder = _ranges.Max(x => x.PriceRange.To);
            var bottomBorder = _ranges.Min(x => x.PriceRange.From);
            
            var bodySpreadInPercents = Math.Abs(topBorder / bottomBorder - 1);

            return bodySpreadInPercents;
        }

        public decimal GetChanelExtension(decimal price)
        {
            var actualSize = Size();
            
            var topBorder = Math.Max(_ranges.Max(x => x.PriceRange.To), price);
            var bottomBorder = Math.Min(_ranges.Min(x => x.PriceRange.From), price);
            
            var newSize = Math.Abs(topBorder / bottomBorder - 1);

            return newSize > actualSize ? newSize - actualSize : decimal.Zero;
        }
    }
}