using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Trading.Analysis.Model;
using Trady.Analysis;
using Trady.Core.Infrastructure;
using Trading.Exchange.Markets.Instruments.Candles;
using Trady.Core;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Trading.Analysis
{
    public abstract class Strategy : IStrategy
    {
        protected readonly Predicate<IIndexedOhlcv> BuyRule;
        protected readonly Predicate<IIndexedOhlcv> SellRule;

        protected Strategy()
        {
            BuyRule = CreateBuyRule();
            SellRule = CreateSellRule();
        }

        protected abstract Predicate<IIndexedOhlcv> CreateSellRule();
        protected abstract Predicate<IIndexedOhlcv> CreateBuyRule();

        public IReadOnlyCollection<Signal> BackTest(IEnumerable<ICandle> ic)
        {
            using var analyzeContext = new AnalyzeContext(ic.Select(x => new Candle(new DateTimeOffset(x.OpenTime), x.Open, x.High, x.Low, x.Close, x.Volume)));
            var result1 = new List<Signal>();
            var shortEntryCandles = new SimpleRuleExecutor(analyzeContext, SellRule).Execute();
            var longEntryCandles = new SimpleRuleExecutor(analyzeContext, BuyRule).Execute();
            var longEntries = SelectEntries(longEntryCandles, Position.Long);
            var shortEntries = SelectEntries(shortEntryCandles, Position.Short);
            return result1.AsReadOnly();
        }


        private IEnumerable<IGrouping<EntryState, Entry>> SelectEntries(IEnumerable<IIndexedOhlcv> ic, Position position) 
        {
            var entries = ic.Select(x => new Entry(x, position)).ToList();
            return entries.GroupBy(x => x.State);
        }
    }
}
