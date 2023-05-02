using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trading.Api.Services;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

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
                new InstrumentName("LTC", "USDT"),
            };
            var range = new Shared.Ranges.Range<DateTime>(new DateTime(2023, 04, 01), new DateTime(2023, 04, 20));

            var brokers = new List<ConnectionEnum>
            {
                ConnectionEnum.Binance,
                ConnectionEnum.Bybit
            };

            var timeframes = new List<Timeframes>
            {
                Timeframes.FiveMinutes,
                Timeframes.OneHour
            };

            var request = new CandlesLoadRequest(brokers, instruments, timeframes, range);
            await _candleService.LoadCandlesToFileAsync(request);

            return Ok();
        }
    }
}
