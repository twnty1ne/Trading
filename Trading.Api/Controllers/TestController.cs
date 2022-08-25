using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var candels = instrument.GetTimeframe(TimeframeEnum.OneHour).GetCandles();
            var averageClose = candels.Average(x => x.Close);
            var averageHigh = candels.Average(x => x.High);
            var averageLow = candels.Average(x => x.Low);
            var name = instrument.Name.BaseCurrencyName;
            return Task.FromResult<IActionResult>(Ok());
        }


        [HttpGet("1")]
        public IActionResult TestMethod1()
        {
            var instrument = _exchange.Market.FuturesUsdt.GetInstrument(new InstrumentName("XRP", "USDT"));
            var candles = instrument.GetTimeframe(TimeframeEnum.OneHour).GetCandles();
            var tradingStrategy = new CandleVolumeStrategy();
            var backtestResult = tradingStrategy.BackTest(candles);
            SaveToFile(instrument.Name, backtestResult);
            return Ok(backtestResult);
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
