using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Instruments.Timeframes.Extentions
{
    public static class TimefrimeExtentions
    {
        public static TimeSpan GetTimeframeTimeSpan(this Timeframes timeframe) 
        {
            var dictionary = new Dictionary<Timeframes, double>
            {
                { Timeframes.OneHour, 60.00 },
                { Timeframes.FourHours, 240.00 },
                { Timeframes.OneDay, 1440.00 },
                { Timeframes.ThirtyMinutes, 30 }
            };
            return TimeSpan.FromMinutes(dictionary.GetValueOrDefault(timeframe));
        }
    }
}
