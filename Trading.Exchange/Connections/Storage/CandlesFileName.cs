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
        private readonly int _chunkIndex;

        public CandlesFileName(ConnectionEnum connection, Timeframes timeframe, IInstrumentName instrumentName, int chunkIndex  = 0)
        {
            Connection = connection;
            Timeframe = timeframe;
            InstrumentName = instrumentName;
            _chunkIndex = chunkIndex;
        }

        public string Value()
        {
            var fileName = $"{Connection.ToString().ToLower()}_{Timeframe.ToString().ToLower()}_{InstrumentName.GetFullName().ToLower()}";

            if (_chunkIndex > 0)
            {
                fileName += $"_chunk_{_chunkIndex}";
            }

            return fileName;
        }
    }
}
