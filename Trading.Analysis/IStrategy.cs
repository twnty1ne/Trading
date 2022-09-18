using System.Collections.Generic;
using Trading.Analysis.Model;

namespace Trading.Analysis
{
    public interface IStrategy
    {
        IReadOnlyCollection<IEntry> BackTest();
    }
}
