using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Connections.Storage
{
    public class CandlesFileName
    {
        private readonly ConnectionEnum Connection;
        private readonly Timeframes Timeframe;
        private readonly IInstrumentName InstrumentName;

        public CandlesFileName(ConnectionEnum connection, Timeframes timeframe, IInstrumentName instrumentName)
        {
            Connection = connection;
            Timeframe = timeframe;
            InstrumentName = instrumentName;
        }

        public string Value()
        {
            return $"{Connection.ToString().ToLower()}_{Timeframe.ToString().ToLower()}_{InstrumentName.GetFullName().ToLower()}";
        }
    }
}
