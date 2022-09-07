using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Trading.Analysis.Model;
using Trading.Analysis.Strategies;
using Trading.Exchange;
using Trading.Exchange.Markets.Instruments;
using Trading.Exchange.Markets.Instruments.Timeframes;
using Trading.Shared.Files;
using Trading.Analytics.Core.Metrics;
using Trading.Analytics.Core;
using Trading.Analysis.Analytics;
using Trading.Analysis.Analytics.Metrics;
using Trading.Analytics.Core.SplitTesting;

namespace Trading.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IExchange _exchange;

        public TestController(IExchange exchange)
        {
            _exchange = exchange;
        }

        [HttpGet]
        public Task<IActionResult> TestMethod() 
        {
            var instrument = _exchange.Market.FuturesUsdt.GetInstrument(new InstrumentName("ETH", "USDT"));
            var candels = instrument.GetTimeframe(Timeframes.OneHour).GetCandles();
            var averageClose = candels.Average(x => x.Close);
            var averageHigh = candels.Average(x => x.High);
            var averageLow = candels.Average(x => x.Low);
            var name = instrument.Name.BaseCurrencyName;
            return Task.FromResult<IActionResult>(Ok());
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


        [HttpGet("5")]
        public IActionResult TestMethod5()
        {
            var metrics = new List<StrategyMetrics>
            {
                StrategyMetrics.WinLossRatio
            };

            var leftEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 2m, 0.004m).BackTest();
            var rightEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 3m, 0.01m).BackTest();
            var splitTest = new SplitTest<IEntry, StrategyMetrics, EntryParameters>(new List<IEstimationParameter<IEntry, StrategyMetrics>> 
            { 
                new EstimationParameter<IEntry, StrategyMetrics>(new WinRateRatioMetric(), Importance.ExtraHigh, EstimationWays.HigherTheBetter, 0m, 1m),
                new EstimationParameter<IEntry, StrategyMetrics>(new TotalNumberOfEntriesMetric(), Importance.ExtraHigh, EstimationWays.HigherTheBetter, 0m, 1000m),
            }, 
                new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>> 
                {
                    new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.004m),
                    new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 2m)
                }, 
                leftEntries),
                new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
                {
                    new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.01m),
                    new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 3m)
                },
                rightEntries)

            );;
            return Ok(splitTest.GetDifference());
        }


        [HttpGet("6")]
        public IActionResult TestMethod6()
        {
            var metrics = new List<StrategyMetrics>
            {
                StrategyMetrics.WinLossRatio
            };

            var leftEntries = new CandleVolumeStrategy(_exchange.Market.FuturesUsdt, 2m, 0.004m).BackTest();
            var splitTest = new CompositeSplitTest<IEntry, StrategyMetrics, EntryParameters>(new List<IEstimationParameter<IEntry, StrategyMetrics>>
            {
                new EstimationParameter<IEntry, StrategyMetrics>(new WinRateRatioMetric(), Importance.ExtraHigh, EstimationWays.HigherTheBetter, 0m, 1m),
                new EstimationParameter<IEntry, StrategyMetrics>(new TotalNumberOfEntriesMetric(), Importance.Low, EstimationWays.HigherTheBetter, 0m, 1000m),
                new EstimationParameter<IEntry, StrategyMetrics>(new Metric<IEntry, StrategyMetrics>(x
                => x.Data.Average(x => x.StopLoss), StrategyMetrics.AverageStopLoss), Importance.ExtraLow, EstimationWays.HigherTheBetter, 0m, 1000m)
            },
            new List<ISelection<EntryParameters, IEntry>>
            {
                new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
                {
                    new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.004m),
                    new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 2m)
                },
                leftEntries),
                new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
                {
                    new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.06m),
                    new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 7m)
                },
                leftEntries.Where(x => x.State != EntryState.HitTakeProfit).ToList()),
                new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
                {
                    new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.003m),
                    new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 8m)
                },
                leftEntries.Where(x => x.State != EntryState.HitStopLoss).ToList()),
                new Selection<EntryParameters, IEntry>(new List<IParameter<EntryParameters, decimal>>
                {
                    new Parameter<EntryParameters, decimal>(EntryParameters.StopLossTreshold, 0.08m),
                    new Parameter<EntryParameters, decimal>(EntryParameters.RiskRescue, 3m)
                },
                leftEntries.Where(x => x.State != EntryState.Skipped).ToList())
            }


            );
            return Ok(splitTest.GetOptimal());
        }

        private void SaveToFile(IInstrumentName name, IReadOnlyCollection<IEntry> entries) 
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "BacktestResults", $"{name.GetFullName()}_{DateTime.UtcNow.Ticks}.txt");
            var jsonValue = JsonConvert.SerializeObject(entries, Formatting.Indented);
            var file = new LocalFile(path, jsonValue);
            file.SaveImmutable();
        }
    }
}
