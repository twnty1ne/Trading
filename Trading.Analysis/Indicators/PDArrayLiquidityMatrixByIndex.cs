using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Trady.Analysis;
using Trady.Analysis.Indicator;
using Trady.Core.Infrastructure;
using Trady.Analysis.Infrastructure;
using Trading.Shared.Ranges;

namespace Trading.Analysis.Indicators
{
    public class PDArrayLiquidityMatrixByIndex : AnalyzableBase<IOhlcv, IOhlcv, IEnumerable<FibonacciRange>, AnalyzableTick<IEnumerable<FibonacciRange>>>
    {
        public PDArrayLiquidityMatrixByIndex(IEnumerable<IOhlcv> inputs) : base(inputs, x => x)
        {
        }

        protected override IEnumerable<FibonacciRange> ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return new PdArrayLiquidityMatrix(mappedInputs, index).Grid;
        }
    }
}
