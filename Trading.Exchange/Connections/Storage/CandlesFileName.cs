using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Exchange.Markets.Core.Instruments;

namespace Trading.Exchange.Connections.Storage
{
    public class CandlesFileName
    {
        private readonly ConnectionEnum _connection;
        private readonly Timeframes _timeframe;
        private readonly IInstrumentName _instrumentName;
        private readonly int _year;
        private readonly int _month;

        public CandlesFileName(ConnectionEnum connection, Timeframes timeframe, IInstrumentName instrumentName, int year, 
            int month)
        {
            _connection = connection;
            _timeframe = timeframe;
            _instrumentName = instrumentName;
            _year = year;
            _month = month;
        }

        public string Value()
        {
            return $"{_connection.ToString().ToLower()}_{_timeframe.ToString().ToLower()}" +
                   $"_{_instrumentName.GetFullName().ToLower()}" +
                   $"_{_year}_{_month}";
        }
    }
}
