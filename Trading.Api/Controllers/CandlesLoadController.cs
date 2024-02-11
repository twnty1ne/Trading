using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trading.Api.Services;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Ranges;

namespace Trading.Api.Controllers
{
    [Route("api/candles/load")]
    [ApiController]
    public class CandlesLoadController : ControllerBase
    {
        private readonly ICandleService _candleService;

        public CandlesLoadController(ICandleService candleService)
        {
            _candleService = candleService ?? throw new ArgumentNullException(nameof(candleService));
        }

        [HttpPost]
        public async Task<IActionResult> LoadCandles()
        {
            var instruments = new List<IInstrumentName>
            {
                new InstrumentName("ETH", "USDT"),
                new InstrumentName("BTC", "USDT"),
                new InstrumentName("XRP", "USDT"),
                new InstrumentName("ADA", "USDT"),
                new InstrumentName("SOL", "USDT"),
                new InstrumentName("LTC", "USDT"),
                new InstrumentName("BNB", "USDT"),
                new InstrumentName("ETC", "USDT"),
                new InstrumentName("UNI", "USDT"),
                new InstrumentName("LINK", "USDT"),
                new InstrumentName("NEAR", "USDT"),
                new InstrumentName("ATOM", "USDT"),
            };
            var range = new Range<DateTime>(new DateTime(2023, 04, 01), 
                new DateTime(2023, 08, 1), BoundariesComparation.LeftIncluding);

            var brokers = new List<ConnectionEnum>
            {
                ConnectionEnum.Binance,
            };

            var timeframes = new List<Timeframes>
            {
                Timeframes.OneMinute,
                Timeframes.OneHour
            };

            var request = new CandlesLoadRequest(brokers, instruments, timeframes, range);
            await _candleService.LoadCandlesToFileAsync(request);

            return Ok();
        }
    }
}
