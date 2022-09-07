using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Analytics.Core.SplitTesting
{
    public interface IEstimation<T, R> where R : Enum
    {
        decimal Estimate(ISelection<T> selection);

    }
}
