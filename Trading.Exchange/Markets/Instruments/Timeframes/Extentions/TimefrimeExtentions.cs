using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Exchange.Markets.Instruments.Timeframes.Extentions
{
    public static class TimefrimeExtentions
    {
        public static TimeSpan GetTimeframeTimeSpan(this TimeframeEnum timeframe) 
        {
            var dictionary = new Dictionary<TimeframeEnum, double>
            {
                { TimeframeEnum.OneHour, 60.00 },
                { TimeframeEnum.FourHours, 240.00 },
                { TimeframeEnum.OneDay, 1440.00 }
            };
            return TimeSpan.FromMinutes(dictionary.GetValueOrDefault(timeframe));
        }
    }
}
