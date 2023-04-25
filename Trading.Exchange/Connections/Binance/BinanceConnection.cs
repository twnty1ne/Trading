using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trading.Exchange.Authentification;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Binance;
using Trading.Exchange.Connections.Binance.Extentions;
using Trading.Exchange.Connections.Storage;
using Trading.Exchange.Connections.Ticker;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;
using Trading.Exchange.Storage;
using Trading.Shared.Excel;
using Trading.Shared.Ranges;

namespace Trading.Connections.Binance
{
    public sealed class BinanceConnection : BaseConnection
    {
        private readonly IBinanceClient _client = new BinanceClient();
        private readonly IBinanceSocketClient _socketClient = new BinanceSocketClient();
        private readonly IExchangeInfoStorage _storage = new ExchangeInfoStorage();

        public BinanceConnection(ICredentialsProvider credentialProvider) : base(credentialProvider, ConnectionEnum.Binance)
        {
        }

        public override ConnectionEnum Type => ConnectionEnum.Binance;

        public async override Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe)
        {
            var range = new Range<DateTime>(new DateTime(2023, 01, 1), new DateTime(2023, 01, 31, 23, 59, 59));

            return await GetFuturesCandlesAsync(name, timeframe, range);
        }


        public async override Task<IReadOnlyCollection<ICandle>> GetFuturesCandlesAsync(IInstrumentName name, Timeframes timeframe, IRange<DateTime> range)
        {
            if (TryGetFromStorage(name, timeframe, range, out var candles)) 
            {
                return candles;
            }

            KlineInterval convertedTimeframe;
            var successfullyConverted = timeframe.TryConvertToBinanceTimeframe(out convertedTimeframe);

            if (!successfullyConverted) throw new ArgumentException("Invalid timeframe");

            var timeframeTicks = timeframe.GetTimeframeTimeSpan().Ticks;

            var result = new List<IBinanceKline>();

            var limit = 1000;
            var lastResultItemsAmount = 0;

            var lastEndDate = range.To;

            while (lastResultItemsAmount == 0 || lastResultItemsAmount == limit && range.Contains(lastEndDate))
            {
                var response = await _client.UsdFuturesApi.ExchangeData
                    .GetKlinesAsync($"{name.BaseCurrencyName}{name.QuoteCurrencyName}", convertedTimeframe, limit: limit, endTime: lastEndDate);

                if (!response.Success) throw new Exception($"status code: {response.ResponseStatusCode}, message: {response.Error}");

                result.AddRange(response.Data.Where(x => range.Contains(x.OpenTime)));
                lastResultItemsAmount = response.Data.Count();

                lastEndDate = lastEndDate.AddTicks(-timeframeTicks * limit);
            }

            return result.OrderBy(x => x.CloseTime)
                .Select(x => new Candle(x.OpenPrice, x.ClosePrice, x.HighPrice, x.LowPrice, x.Volume, x.OpenTime, x.CloseTime))
                .ToList().AsReadOnly();
        }

        public override IInstrumentStream GetHistoryInstrumentStream(IInstrumentName name, IMarketTicker ticker)
        {
            return new BinanceHistoryInstrumentStream(this, name, _socketClient, ticker);
        }

        public override IInstrumentStream GetInstrumentStream(IInstrumentName name) 
        {
            return new BinanceInstrumentStream(this, name, _socketClient);
        }

        private bool TryGetFromStorage(IInstrumentName name, Timeframes timeframe, IRange<DateTime> range, out IReadOnlyCollection<ICandle> candles) 
        {
            var infoRead = _storage.TryGetSymbol(name, Type, out var info);

            if (!infoRead)
            {
                candles = Enumerable.Empty<ICandle>().ToList().AsReadOnly();
                return false;
            }

            if (info.FirstCandleDate >= range.To) 
            {
                candles = Enumerable.Empty<ICandle>().ToList().AsReadOnly();
                return true;
            }

            if (range.From < info.FirstCandleDate)
            {
                range = new Range<DateTime>(info.FirstCandleDate, range.To);
            }

            _storage.TryGetCandles(name, Type, timeframe, out var storageCandles);

            var candleRange = new CandlesRange(storageCandles, timeframe);

            if (candleRange.FullFilled(range))
            {
                candles = storageCandles.Where(x => range.Contains(x.CloseTime)).ToList().AsReadOnly();
                return true;
            }

            candles = Enumerable.Empty<ICandle>().ToList().AsReadOnly();
            return false; 
        }
    }
} 
