using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Markets.Core.Instruments.Candles
{
    public interface ICandle
    {
        decimal Open { get; }
        decimal Close { get; }
        decimal High { get; }
        decimal Low { get; }
        decimal Volume { get; }
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
    }
}
