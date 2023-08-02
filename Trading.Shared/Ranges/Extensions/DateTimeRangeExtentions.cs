using System;
using System.Collections.Generic;
using System.Globalization;

namespace Trading.Shared.Ranges.Extensions
{
    public static class DateTimeRangeExtensions
    {
        public static IEnumerable<(int Year, int Month)> MonthsBetween(this IRange<DateTime> range)
        {
            var iterator = new DateTime(range.From.Year, range.From.Month, 1);
            var limit = range.To;
            
            while (iterator <= limit)
            {
                yield return (
                    iterator.Year, 
                    iterator.Month
                );

                iterator = iterator.AddMonths(1);
            }
        }
    }
}