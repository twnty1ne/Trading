using System;
using System.Collections.Generic;
using System.Linq;
using Trading.Analysis.Model;
using Trady.Analysis;
using Trady.Core.Infrastructure;
using Trady.Core;
using Trading.Exchange.Markets;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Timeframes;

namespace Trading.Analysis
{
    public abstract class Strategy : IStrategy
    {
        private readonly IMarket<IFuturesInstrument> _market;
        private readonly IReadOnlyCollection<Timeframes> _supportedTimeFrames;
        private readonly IReadOnlyCollection<IInstrumentName> _supportedInstruments;
        private readonly decimal _slTreshold;
        private readonly decimal _mathExpectation;

        protected readonly Predicate<IIndexedOhlcv> BuyRule;
        protected readonly Predicate<IIndexedOhlcv> SellRule;


        protected Strategy(IMarket<IFuturesInstrument> market, decimal mathExpectation, decimal slTreshold)
        {
            BuyRule = CreateBuyRule();
            SellRule = CreateSellRule();
            _supportedInstruments = CreateSupportedInstrumentsList();
            _supportedTimeFrames = CreateSupportedTimeframesList();
            _market = market ?? throw new ArgumentNullException(nameof(market));
            _mathExpectation = mathExpectation;
            _slTreshold = slTreshold;
        }

        protected abstract Predicate<IIndexedOhlcv> CreateSellRule();
        protected abstract Predicate<IIndexedOhlcv> CreateBuyRule();


        public IReadOnlyCollection<IEntry> BackTest()
        {
            var instruments = _supportedInstruments.Select(x => _market.GetInstrument(x));
            return instruments.SelectMany(x => BackTest(x)).ToList().AsReadOnly();
        }

        private IReadOnlyCollection<IEntry> BackTest(IInstrument instrument)
        {
            var timeFrames = _supportedTimeFrames.Select(x => instrument.GetTimeframe(x));
            return timeFrames.SelectMany(x => BackTest(x)).ToList().AsReadOnly();
        }


        private IReadOnlyCollection<IEntry> BackTest(ITimeframe timeframe) 
        {
            var candles = timeframe.GetCandles();
            using var analyzeContext = new AnalyzeContext(candles.Select(x => new Candle(new DateTimeOffset(x.OpenTime), x.Open, x.High, x.Low, x.Close, x.Volume)));
            var shortEntryCandles = new SimpleRuleExecutor(analyzeContext, SellRule).Execute(candles.Count() - 200);
            var longEntryCandles = new SimpleRuleExecutor(analyzeContext, BuyRule).Execute(candles.Count() - 200);
            var result = new List<Entry>();
            var longEntries = SelectEntries(longEntryCandles.Where(x => x.Next != null), Position.Long);
            var shortEntries = SelectEntries(shortEntryCandles.Where(x => x.Next != null), Position.Short);
            result.AddRange(longEntries.SelectMany(x => x.AsEnumerable()));
            result.AddRange(shortEntries.SelectMany(x => x.AsEnumerable()));
            return result.AsReadOnly();
        }

        private IEnumerable<IGrouping<EntryState, Entry>> SelectEntries(IEnumerable<IIndexedOhlcv> ic, Position position)
        {
            var entries = ic.Select(x => new Entry(x, position, _mathExpectation, _slTreshold)).ToList();
            return entries.GroupBy(x => x.State);
        }

        private IReadOnlyCollection<IInstrumentName> CreateSupportedInstrumentsList()
        {
            return new List<IInstrumentName>
            {
                new InstrumentName("XRP", "USDT"),
                //new InstrumentName("LTC", "USDT"),
                //new InstrumentName("ETH", "USDT"),
                //new InstrumentName("ETC", "USDT")
            };
        }

        private IReadOnlyCollection<Timeframes> CreateSupportedTimeframesList()
        {
            return new List<Timeframes>
            {
                //Timeframes.FourHours,
                Timeframes.OneHour,
                //Timeframes.ThirtyMinutes
            };
        }


    }
}
