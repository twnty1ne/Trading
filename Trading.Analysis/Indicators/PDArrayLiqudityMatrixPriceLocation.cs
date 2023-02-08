using System;
using System.Collections.Generic;
using System.Text;
using Trady.Analysis;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;
using System.Linq;

namespace Trading.Analysis.Indicators
{
    public class PDArrayLiqudityMatrixPriceLocation : AnalyzableBase<IOhlcv, decimal, FibonacciRange, AnalyzableTick<FibonacciRange>>
    {
        private PDArrayLiquidityMatrixByIndex _pdArrayLiquidityMatrixByIndex;

        public PDArrayLiqudityMatrixPriceLocation(IEnumerable<IOhlcv> inputs, Func<IOhlcv, decimal> inputMapper) : base(inputs, inputMapper)
        {
            _pdArrayLiquidityMatrixByIndex = new PDArrayLiquidityMatrixByIndex(inputs);
        }

        protected override FibonacciRange ComputeByIndexImpl(IReadOnlyList<decimal> mappedInputs, int index)
        {
            var pdArrayMatrix = _pdArrayLiquidityMatrixByIndex[index].Tick;
            var price = mappedInputs[index];
            var fibonacciRange = pdArrayMatrix.FirstOrDefault(x => x.PriceRange.Contains(price));
            if (fibonacciRange is null) throw new Exception();
            return fibonacciRange;
        }
    }
}
