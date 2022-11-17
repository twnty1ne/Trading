using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Binance;
using Trading.Exchange.Connections.Binance.Extentions;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;

namespace Trading.Connections.Binance
{
    public sealed class BinanceConnection : BaseConnection
    {
        private readonly IBinanceClient _client = new BinanceClient();
        private readonly IBinanceSocketClient _socketClient = new BinanceSocketClient();

        public BinanceConnection(ICredentialsProvider credentialProvider) : base(credentialProvider, ConnectionEnum.Binance)
        {
        }

        public async override Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe)
        {
            KlineInterval convertedTimeframe;
            var successfullyConverted = timeframe.TryConvertToBinanceTimeframe(out convertedTimeframe);
            if (!successfullyConverted) throw new ArgumentException("Invalid timeframe");
            var result = new List<IBinanceKline>();
            var limit = 1000;
            var lastResultItemsAmount = 0;
            var lastEndDate = DateTime.UtcNow;
            var timeframeTicks = timeframe.GetTimeframeTimeSpan().Ticks;
            while (lastResultItemsAmount == 0 || lastResultItemsAmount == limit)
            {
                var response = await _client.UsdFuturesApi.ExchangeData
                    .GetKlinesAsync($"{name.BaseCurrencyName}{name.QuoteCurrencyName}", convertedTimeframe, limit: limit, endTime: lastEndDate);
                if (!response.Success) throw new Exception($"status code: {response.ResponseStatusCode}, message: {response.Error}");
                result.AddRange(response.Data);
                lastResultItemsAmount = response.Data.Count();
                lastEndDate = lastEndDate.AddTicks(-timeframeTicks * limit);
            }
            return result.OrderBy(x => x.CloseTime).Select(x => new Candle(x.OpenPrice, x.ClosePrice, x.HighPrice, x.LowPrice, x.Volume, x.OpenTime, x.CloseTime)).ToList().AsReadOnly();
        }

        public override IInstrumentStream GetHistoryInstrumentStream(IInstrumentName name, IMarketTicker ticker)
        {
            return new BinanceHistoryInstrumentStream(this, name, _socketClient, ticker);
        }

        public override IInstrumentStream GetInstrumentStream(IInstrumentName name) 
        {
            return new BinanceInstrumentStream(this, name, _socketClient);
        }
    }
} 
