using Bybit.Net.Clients;
using Bybit.Net.Enums;
using Bybit.Net.Interfaces.Clients;
using Bybit.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Binance;
using Trading.Exchange.Connections.Bybit;
using Trading.Exchange.Connections.Bybit.Extentions;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;
using Trading.Shared.Ranges;

namespace Trading.Connections.Bybit
{
    public sealed class BybitConnection : BaseConnection
    {
        private readonly IBybitClient _client = new BybitClient();
        private readonly IBybitSocketClient _socketClient = new BybitSocketClient();

        public BybitConnection(ICredentialsProvider credentialProvider) : base(credentialProvider, ConnectionEnum.Binance)
        {
        }

        public async override Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe)
        {
            var successfullyConverted = timeframe.TryConvertToBybitTimeframe(out var convertedTimeframe);

            if (!successfullyConverted) throw new ArgumentException("Invalid timeframe");

            var result = new List<BybitKline>();

            var limit = 200;
            var lastResultItemsAmount = 0;

            var from = new DateTime(2023, 01, 1);
            var to = new DateTime(2023, 01, 31, 23, 59, 59);
            var range = new Range<DateTime>(from, to);

            var lastEndDate = from;
            var timeframeTicks = timeframe.GetTimeframeTimeSpan().Ticks;

            while (lastResultItemsAmount == 0 || lastResultItemsAmount == limit && range.Contains(lastEndDate))
            {
                var response = await _client.UsdPerpetualApi.ExchangeData.GetKlinesAsync(name.GetFullName(), convertedTimeframe, lastEndDate, limit: limit);

                if (!response.Success) throw new Exception($"status code: {response.ResponseStatusCode}, message: {response.Error}");

                result.AddRange(response.Data.Where(x => range.Contains(x.OpenTime)));
                lastResultItemsAmount = response.Data.Count();

                lastEndDate = lastEndDate.AddTicks(timeframeTicks * limit);
            }

            return result.OrderBy(x => x.OpenTime)
                .Select(x => SelectCandle(x, timeframe))
                .ToList()
                .AsReadOnly();
        }

        public override IInstrumentStream GetHistoryInstrumentStream(IInstrumentName name, IMarketTicker ticker)
        {
            return new BybitHistoryInstrumentStream(this, name, _socketClient, ticker);
        }

        public override IInstrumentStream GetInstrumentStream(IInstrumentName name) 
        {
            return new BybitInstrumentStream(this, name, _socketClient);  
        }

        private Candle SelectCandle(BybitKline kline, Timeframes timeframe) 
        {
            var timeframeTicks = timeframe.GetTimeframeTimeSpan().Ticks;

            return new Candle(kline.OpenPrice, kline.ClosePrice, kline.HighPrice, kline.LowPrice, kline.Volume, kline.OpenTime.ToUniversalTime(),
                kline.OpenTime.ToUniversalTime().AddTicks(timeframeTicks - TimeSpan.TicksPerSecond));
        }
    }
} 
