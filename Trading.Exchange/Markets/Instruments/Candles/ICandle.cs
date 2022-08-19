using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Instruments.Candles
{
    public interface ICandle
    {
        decimal Open { get; }
        decimal Close { get; }
        decimal High { get; }
        decimal Low { get; }
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
    }
}
