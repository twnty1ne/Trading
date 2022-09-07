using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Trading.Analytics.Core
{
    public class SplitTestResult<TSelection, R, TParameter>
        where TParameter : Enum
        where R : Enum

    {
        public SplitTestResult(ISelection<TParameter, TSelection> leftSelection, ISelection<TParameter, TSelection> rightSelection, IReadOnlyCollection<IMetricResult<R>> difference)
        {
            LeftSelection = leftSelection ?? throw new ArgumentNullException(nameof(leftSelection));
            RightSelection = rightSelection ?? throw new ArgumentNullException(nameof(rightSelection));
            Difference = difference ?? throw new ArgumentNullException(nameof(difference));
        }

        public ISelection<TParameter, TSelection> LeftSelection { get; private set; }
        public ISelection<TParameter, TSelection> RightSelection { get; private set; }
        public IReadOnlyCollection<IMetricResult<R>> Difference { get; private set; }
    }
}
