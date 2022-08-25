using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Trading.Analysis.Model;
using Trading.Exchange.Markets.Instruments.Candles;
using Trady.Core.Infrastructure;

namespace Trading.Analysis
{
    public interface IStrategy
    {
        IReadOnlyCollection<IEntry> BackTest(IEnumerable<ICandle> ic);
    }
}
