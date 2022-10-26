using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Trading.Bot;
using Trading.Exchange;

namespace Trading.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IExchange _exchange;
        private readonly IBot _bot;

        public TestController(IExchange exchange, IBot bot)
        {
            _exchange = exchange;
            _bot = bot;
        }


        [HttpGet("1")]
        public IActionResult TestMethod1()
        {
            //var instrument = _exchange.Market.FuturesUsdt.GetInstrument(new InstrumentName("XRP", "USDT"));
            //var candles = instrument.GetTimeframe(Timeframes.OneHour).GetCandles();
            //var tradingStrategy = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt);
            //return Ok();
            return Ok();
        }


        [HttpGet("4")]
        public IActionResult TestMethod4()
        {
            //var metrics = new List<StrategyMetrics>
            //{
            //    StrategyMetrics.WinLossRatio
            //};

            //var entries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt).BackTest();
            //var analytics = new StrategyAnalytics(entries, metrics);
            //return Ok(analytics.GetResults());
            return Ok();
        }


        //[HttpGet("5")]
        //public IActionResult TestMethod5()
        //{
        //    var metrics = new List<StrategyMetrics>
        //    {
        //        StrategyMetrics.WinLossRatio
        //    };

        //    var leftEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 2m, 0.004m).BackTest();
        //    var rightEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 3m, 0.01m).BackTest();
        //    var splitTest = new SplitTest<IEntry, StrategyMetrics, EntryParameters>(new List<IEstimationParameter<IEntry, StrategyMetrics>> 
        //    { 
        //        new EstimationParameter<IEntry, StrategyMetrics>(new WinRateRatioMetric(), Importance.ExtraHigh, EstimationWays.HigherTheBetter, 0m, 1m),
        //        new EstimationParameter<IEntry, StrategyMetrics>(new TotalNumberOfEntriesMetric(), Importance.ExtraHigh, EstimationWays.HigherTheBetter, 0m, 1000m),
        //    }, 
        //        new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>> 
        //        {
        //            new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.004m),
        //            new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 2m)
        //        }, 
        //        leftEntries),
        //        new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
        //        {
        //            new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.01m),
        //            new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 3m)
        //        },
        //        rightEntries)

        //    );;
        //    return Ok(splitTest.GetDifference());
        //}


        //[HttpGet("6")]
        //public IActionResult TestMethod6()
        //{
        //    var metrics = new List<StrategyMetrics>
        //    {
        //        StrategyMetrics.WinLossRatio
        //    };

        //    var leftEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 2m, 0.004m).BackTest();
        //    var splitTest = new CompositeSplitTest<IEntry, StrategyMetrics, EntryParameters>(new List<IEstimationParameter<IEntry, StrategyMetrics>>
        //    {
        //        new EstimationParameter<IEntry, StrategyMetrics>(new WinRateRatioMetric(), Importance.ExtraHigh, EstimationWays.HigherTheBetter, 0m, 1m),
        //        new EstimationParameter<IEntry, StrategyMetrics>(new TotalNumberOfEntriesMetric(), Importance.Low, EstimationWays.HigherTheBetter, 0m, 1000m),
        //        new EstimationParameter<IEntry, StrategyMetrics>(new Metric<IEntry, StrategyMetrics>(x
        //        => x.Data.Average(x => x.StopLoss), StrategyMetrics.AverageStopLoss), Importance.ExtraLow, EstimationWays.HigherTheBetter, 0m, 1000m)
        //    },
        //    new List<ISelection<EntryParameters, IEntry>>
        //    {
        //        new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
        //        {
        //            new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.004m),
        //            new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 2m)
        //        },
        //        leftEntries),
        //        new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
        //        {
        //            new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.06m),
        //            new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 7m)
        //        },
        //        leftEntries.Where(x => x.State != EntryState.HitTakeProfit).ToList()),
        //        new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
        //        {
        //            new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.003m),
        //            new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 8m)
        //        },
        //        leftEntries.Where(x => x.State != EntryState.HitStopLoss).ToList()),
        //        new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
        //        {
        //            new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.08m),
        //            new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 3m)
        //        },
        //        leftEntries.Where(x => x.State != EntryState.Skipped).ToList())
        //    }


        //    );
        //    return Ok(splitTest.GetOptimal());
        //}


        [HttpGet("7")]
        public IActionResult TestMethod7()
        {
            //var leftEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 2m, 0.004m).BackTest();
            //var builder = new DecisionMakingBuilder<IEntry, StrategyMetrics, EntryParameters>();
            //var dm = builder
            //    .AnalyticHierarchyProcess()
            //    .Criterias()
            //    .HasCriteria(new WinRateRatioMetric(), 0, 1)
            //    .HigherTheBetter()
            //    .WithImportanceLevel(Importance.ExtraHigh)
            //    .HasCriteria(new TotalNumberOfEntriesMetric(), 0, 100)
            //    .HigherTheBetter()
            //    .WithImportanceLevel(Importance.High)
            //    .Alternatives()
            //    .HasAlternative(leftEntries)
            //    .WithParameters(new List<IParameter<EntryParameters, decimal>> 
            //    {
            //        new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.007m),
            //        new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 3m)
            //    })
            //    .HasAlternative(leftEntries.Where(x => x.State == EntryState.HitStopLoss).ToList())
            //    .WithParameters(new List<IParameter<EntryParameters, decimal>>
            //    {
            //        new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.003m),
            //        new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 3m)
            //    })
            //    .HasAlternative(leftEntries.Where(x => x.State == EntryState.HitTakeProfit).ToList())
            //    .WithParameters(new List<IParameter<EntryParameters, decimal>>
            //    {
            //        new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.007m),
            //        new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 2m)
            //    })
            //    .LastOne()
            //    .Build();
            //var dec = dm.Decide();
            return Ok();  
        }


        [HttpGet("8")]
        public IActionResult TestMethod8()
        {
            _bot.Session.OnStopped += (x, y) => Debug.WriteLine(JsonConvert.SerializeObject(y, Formatting.Indented));
            _bot.Session.Start();
            return Ok();
        }


        [HttpGet("9")]
        public IActionResult TestMethod9()
        {
            var sb = new StringBuilder()
                .AppendLine("Instrument: XRPUSDT")
                .AppendLine("Side: Short")
                .AppendLine("SL: 0,4355352")
                .AppendLine("TP: 0,4268592")
                .AppendLine("Price: 0,4338");
            var bot = new TelegramBotClient("5730777041:AAEB8X_UIbVFIkI5JaX1MBKcN_JMQg6-FWY").SendTextMessageAsync(new ChatId(-1001636388029), sb.ToString()).GetAwaiter().GetResult();
            return Ok(bot);
        }


        [HttpGet("10")]
        public IActionResult TestMethod10()
        {
            _bot.Session.Stop();
            return Ok();
        }
    }
}
