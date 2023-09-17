using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Trading.Report.DAL;
using Trading.Bot;
using Trading.Exchange;
using Trading.Report.Core;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trading.Bot.Strategies.CandleVolume.Filters;
using Trading.Exchange.Connections.Bybit;
using Trading.Shared.Ranges;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Builder;
using Trading.Exchange.Markets.Core.Instruments.Timeframes.Extentions;
using Trading.MlClient;
using Trading.MlClient.Resources.Models.InsideChannelLong;
using Trading.MlClient.Resources.Models.InsideChannelShort;
using Trading.MlClient.Resources.Models.OutsideChannel;
using Trading.Researching.Core.DecisionMaking.Splitting.Algorithms.DecisionTree.Nodes.DecisionNodes;


namespace Trading.Api.Controllers
{
    

    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private enum SignalClassification 
        {
            OutsideChannel = 1,
            InsideChannelShort = 2,
            InsideChannelLong = 3
        }
        
        
        private readonly IExchange _exchange;
        private readonly IBot _bot;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMlClient _mlClient;

        public TestController(IBot bot, IServiceScopeFactory scopeFactory, IMlClient mlClient)
        {
            // _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
            _mlClient = mlClient;
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
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
            _bot.Session.OnStopped += (x, y) =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var t = y.Analytics.GetResults();

                    var instrumentRepository = scope.ServiceProvider.GetService<IRepository<Instrument>>();
                    var sessionRepository = scope.ServiceProvider.GetService<IRepository<Session>>();
                    var timeframeRepository = scope.ServiceProvider.GetService<IRepository<Timeframe>>();
                    var strategyRepository = scope.ServiceProvider.GetService<IRepository<Strategy>>();

                    var instruments = instrumentRepository.GetAll();
                    var strategies = strategyRepository.GetAll();
                    var timeframes = timeframeRepository.GetAll();

                    var session = new Session
                    {
                        Trades = y.Trades.Select(x => new Trade
                        {
                            StrategyId = strategies.First(y => y.Type == x.Strategy).Id,
                            TimeframeId = timeframes.First(y => y.Type == x.Timeframe).Id,
                            Position = new Position 
                            {
                                IMR = x.Position.IMR,
                                TakeProfit = x.Position.TakeProfit,
                                Side = x.Position.Side,
                                Leverage = x.Position.Leverage,
                                State = x.Position.State,
                                ROE = x.Position.ROE,
                                RealizedPnl = x.Position.RealizedPnl,
                                Size = x.Position.Size,
                                EntryPrice = x.Position.EntryPrice,
                                InitialMargin = x.Position.InitialMargin,
                                StopLoss = x.Position.StopLoss,
                                EntryDate = x.Position.EntryDate,
                                CloseDate = x.Position.CloseDate,
                                InstrumentId = instruments.First(y => y.Name == x.Position.InstrumentName.GetFullName()).Id,
                                EntryDateTicks = x.Position.EntryDate.Ticks,
                                EntryDateStringValue = x.Position.EntryDate.ToString("G"),
                                Ticks = x.Position.Ticks.Select(z => new PositionPriceTick
                                {
                                    DateTime = z.DateTime,
                                    Price = z.Price
                                }).ToList()
                            },
                            Candles = x.Signal.Candle.BackingList.TakeLast(300).Select(z => new TradeCandle
                            {
                                Close = z.Close,
                                Open = z.Open,
                                High = z.High,
                                Low = z.Low,
                                Volume = z.Volume,
                                OpenTime = z.DateTime.UtcDateTime,
                                CloseTime = z.DateTime.UtcDateTime.Add(x.Timeframe.GetTimeframeTimeSpan())
                            }).ToList()
                        }).ToList()
                    };

                    Console.WriteLine("Adding");
                    
                    sessionRepository.Add(session);
                    sessionRepository.SaveChanges();
                } 
                
            };
            
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

            var bot = new TelegramBotClient("5730777041:AAEB8X_UIbVFIkI5JaX1MBKcN_JMQg6-FWY")
                .SendDocumentAsync(new ChatId(-1001636388029), sb.ToString()).GetAwaiter().GetResult();

            return Ok(bot);
        }

        [HttpGet("10")]
        public async Task<IActionResult> TestMethod10()
        {
            var connection = new BybitConnection(new BinanceCredentialsProvider());

            var range = new Range<DateTime>(new DateTime(2023, 04, 14, 07, 00, 00), 
                new DateTime(2023, 04, 25, 20, 59, 59));

            var candles = await connection
                .GetFuturesCandlesAsync(new InstrumentName("LTC", "USDT"), 
                    Timeframes.OneHour, range);

            return Ok(candles);
        }
        
        [HttpGet("11")]
        public void TestMethod11()
        {
            var builder = new DecisionTreeBuilder<CandleVolumeStrategyContext, SignalClassification>();
            var doc = XElement.Load("CandleVolumeDecisionTree.xml");
            var tree = builder.FromXml(doc);
            var i = 0;
        }
        
        
        [HttpGet("12")]
        public async Task<IActionResult> TestMethod12()
        {
            var insideShortFeatures = new List<(InsideChannelShortFeatures Type, decimal Value)>
            {
                (InsideChannelShortFeatures.DayTime, 12m),
                (InsideChannelShortFeatures.EquilibriumDistance, 0.6m),
                (InsideChannelShortFeatures.PdSize, 0.02m),
            };
            
            var insideShortPredict = await _mlClient.InsideShortModelResource.PredictAsync(insideShortFeatures);
            
            
            var insideLongFeatures = new List<(InsideChannelLongFeatures Type, decimal Value)>
            {
                (InsideChannelLongFeatures.DayTime, 12m),
                (InsideChannelLongFeatures.EquilibriumDistance, 0.06m),
                (InsideChannelLongFeatures.PdSize, 0.002m),
            };


            var insideLongPredict = await _mlClient.InsideLongModelResource.PredictAsync(insideLongFeatures);
            
            
            var outsideFeatures = new List<(OutsideChannelFeatures Type, decimal Value)>
            {
                (OutsideChannelFeatures.PdSize, 12m),
                (OutsideChannelFeatures.TakeProfitChannelExtension, 0.02m),
            };
            
            var outsidePredict = await _mlClient.OutsideModelResource.PredictAsync(outsideFeatures);

            return Ok(new { insideShortPredict, insideLongPredict, outsidePredict });
        }
    }
}
