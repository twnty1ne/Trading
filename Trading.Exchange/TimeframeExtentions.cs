using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Timeframes;
using Trading.Shared.Resolvers;

namespace Trading.Infrastructure.Exchange.Connections.Binance.Extentions
{
    internal static class TimeframeExtentions
    {
        public static bool TryConvertToBinanceTimeframe(this TimeframeEnum timeframe, out KlineInterval binanceTimeframe)
        {

            var dictionary = new Dictionary<TimeframeEnum, Func<KlineInterval>>
            {
                { TimeframeEnum.OneHour, () => KlineInterval.OneHour},
                { TimeframeEnum.FourHours, () => KlineInterval.FourHour},
                { TimeframeEnum.OneDay, () => KlineInterval.OneDay}
            };
            var resolver = new Resolver<TimeframeEnum, KlineInterval>(dictionary);
            return resolver.TryResolve(timeframe, out binanceTimeframe);
        }
    }
}
