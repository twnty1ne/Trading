using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments
{
    public interface IPriceTick
    {
        decimal Price { get; }
        DateTime DateTime { get; }
    }
}
