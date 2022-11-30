using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core
{
    public interface IBalance
    {
        decimal NetVolume { get; }
        decimal GetPercent(decimal percent);

    }
}
