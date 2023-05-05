using System;
using System.Collections.Generic;
using Trady.Analysis;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;

namespace Trading.Analysis.Candlesticks
{
    public class Doji<TInput, TOutput> : AnalyzableBase<TInput, (decimal Open, decimal High, decimal Low, decimal Close), bool, TOutput>
    {
        protected Doji(IEnumerable<TInput> inputs, Func<TInput, (decimal Open, decimal High, decimal Low, decimal Close)> inputMapper) : base(inputs, inputMapper)
        {
        }

        protected override bool ComputeByIndexImpl(IReadOnlyList<(decimal Open, decimal High, decimal Low, decimal Close)> mappedInputs, int index)
        {
            var close = mappedInputs[index].Close;
            var open = mappedInputs[index].Open;
            var low = mappedInputs[index].Low;
            var high = mappedInputs[index].High;
            
            var bodySpreadInPercents = Math.Abs(Math.Max(open, close) / Math.Min(open, close) - 1) * 100;
            var lowerShadowInPercents = Math.Abs(Math.Min(open, close) / low - 1) * 100;
            var higherShadowInPercents = Math.Abs(high / Math.Max(open, close) - 1) * 100;
            
            return bodySpreadInPercents <= 0.05m && lowerShadowInPercents <= 0.6m && higherShadowInPercents <= 0.6m;
        }
    }

    public class Doji : Doji<IOhlcv, AnalyzableTick<bool>>
    {
        public Doji(IEnumerable<IOhlcv> inputs)
            : base(inputs, i => (i.Open, i.High, i.Low, i.Close))
        {
        }
    }
}
