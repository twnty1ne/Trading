using System;
using System.Collections.Generic;
using System.Text;
using Trady.Core.Infrastructure;

namespace Trading.Exchange.Markets.Core.Instruments.Candles
{
    internal class Candle : ICandle
    {
        public Candle(decimal open, decimal close, decimal high, decimal low, decimal volume, DateTime openTime, DateTime closeTime)
        {
            Open = open;
            Close = close;
            High = high;
            Low = low;
            OpenTime = openTime;
            CloseTime = closeTime;
            Volume = volume;
        }

        public decimal Open { get; private set; }

        public decimal Close { get; private set; }

        public decimal High { get; private set; }

        public decimal Low { get; private set; }

        public DateTime OpenTime { get; private set; }

        public DateTime CloseTime { get; private set; }

        public decimal Volume { get; private set; }
    }
}
