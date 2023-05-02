using System;
using System.Collections.Generic;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Ranges;

namespace Trading.Api.Services
{
    public interface ICandlesLoadRequest
    {
        IEnumerable<ConnectionEnum> Brokers { get; }
        IEnumerable<IInstrumentName> Instrument { get; }
        IEnumerable<Timeframes> Timeframe { get; }
        IRange<DateTime> Range { get; }
    }
}
