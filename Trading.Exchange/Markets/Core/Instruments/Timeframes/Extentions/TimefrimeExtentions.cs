using System;
using System.Collections.Generic;

namespace Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions
{
    public static class TimefrimeExtentions
    {
        public static TimeSpan GetTimeframeTimeSpan(this Timeframes timeframe) 
        {
            var dictionary = new Dictionary<Timeframes, double>
            {
                { Timeframes.OneHour, 60.00d },
                { Timeframes.FourHours, 240.00d },
                { Timeframes.OneDay, 1440.00d },
                { Timeframes.ThirtyMinutes, 30.00d },
                { Timeframes.FiveMinutes, 5.00d },
                { Timeframes.OneMinute, 1.00d }
            };
            return TimeSpan.FromMinutes(dictionary.GetValueOrDefault(timeframe));
        }
    }
}
