using System;
using System.Collections.Generic;
using System.Linq;

namespace Trading.Shared.Ranges.Extensions
{
    public static class DateTimeRangeExtensions
    {
        public static IEnumerable<(int Year, int Month)> MonthsBetween(this IRange<DateTime> range)
        {
            var iterator = range.From;

            while (range.Contains(iterator))
            {
                yield return (
                    iterator.Year, 
                    iterator.Month
                );

                iterator = iterator.AddMonths(1);
            }
        }


        public static IEnumerable<IRange<DateTime>> MonthChunks(this IRange<DateTime> range)
        {
            var monthBetween = range
                .MonthsBetween()
                .Select(x => new DateTime(x.Year, x.Month, 1)).ToList();
            
            var linkedChunks = new LinkedList<DateTime>(monthBetween);

            return monthBetween.Select(x =>
            {
                var node = linkedChunks.Find(x);

                if (node is null)
                    throw new ArgumentOutOfRangeException();

                return node.Next?.Value is null 
                    ? new Range<DateTime>(node.Value, range.To, range.BoundariesComparation)
                    : new Range<DateTime>(node.Value, node.Next.Value, range.BoundariesComparation);
            });
        }
    }
}