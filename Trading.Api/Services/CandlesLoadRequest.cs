using System;
using System.Collections.Generic;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Ranges;

namespace Trading.Api.Services
{
    public class CandlesLoadRequest : ICandlesLoadRequest
    {
        public CandlesLoadRequest(IEnumerable<ConnectionEnum> brokers,
            IEnumerable<IInstrumentName> instrument,
            IEnumerable<Timeframes> timeframe,
            IRange<DateTime> period)
        {
            Brokers = brokers;
            Instrument = instrument;
            Timeframe = timeframe;
            Range = period;
        }

        public IEnumerable<ConnectionEnum> Brokers { get; }
        public IEnumerable<IInstrumentName> Instrument { get; }
        public IEnumerable<Timeframes> Timeframe { get; }
        public IRange<DateTime> Range { get; }
    }
}
