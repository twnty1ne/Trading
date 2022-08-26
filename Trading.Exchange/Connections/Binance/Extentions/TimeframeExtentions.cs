using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Instruments.Timeframes;
using Trading.Shared.Resolvers;

namespace Trading.Exchange.Connections.Binance.Extentions
{
    internal static class TimeframeExtentions
    {
        public static bool TryConvertToBinanceTimeframe(this Timeframes timeframe, out KlineInterval binanceTimeframe)
        {

            var dictionary = new Dictionary<Timeframes, Func<KlineInterval>>
            {
                { Timeframes.OneHour, () => KlineInterval.OneHour},
                { Timeframes.FourHours, () => KlineInterval.FourHour},
                { Timeframes.OneDay, () => KlineInterval.OneDay}
            };
            var resolver = new Resolver<Timeframes, KlineInterval>(dictionary);
            return resolver.TryResolve(timeframe, out binanceTimeframe);
        }
    }
}
