using System;

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
            OpenTime = new DateTime(openTime.Ticks, DateTimeKind.Utc);
            CloseTime = new DateTime(closeTime.Ticks, DateTimeKind.Utc);;
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
