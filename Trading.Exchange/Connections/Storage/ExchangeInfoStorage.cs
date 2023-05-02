using ExcelMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Trading.Exchange.Connections;
using Trading.Exchange.Connections.Storage;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;

namespace Trading.Exchange.Storage
{
    internal class ExchangeInfoStorage : IExchangeInfoStorage
    {
        private class PocoCandle 
        {
            public decimal Open { get; set; }
            public decimal Close { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public DateTime OpenTime { get; set; }
            public DateTime CloseTime { get; set; }
            public decimal Volume { get; set; }
        }

        private readonly string _candlesPath = 
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Connections", "Storage", "Candles");

        private readonly string _instrumentsPath = 
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Connections", "Storage", "Instruments", "instruments_info.xlsx");

        public ExchangeInfoStorage()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public bool TryGetCandles(IInstrumentName name, ConnectionEnum connection, Timeframes timeframe, out IEnumerable<Candle> candles)
        {
            try 
            {
                var fileName = new CandlesFileName(connection, timeframe, name);
                var filePath = Path.Combine(_candlesPath, fileName.Value());

                if (!File.Exists(filePath)) 
                {
                    candles = Enumerable.Empty<Candle>();
                    return false;
                }

                using var stream = File.OpenRead(filePath);
                using var importer = new ExcelImporter(stream);

                var sheet = importer.ReadSheet();
                var pocoCandles = sheet.ReadRows<PocoCandle>();

                candles =  pocoCandles.Select(x => new Candle(x.Open, x.Close, x.High, x.Low, x.Volume, x.OpenTime, x.CloseTime))
                    .OrderBy(x => x.OpenTime)
                    .ToList();

                return true;
            }
            catch (Exception ex)
            {
                candles = Enumerable.Empty<Candle>();
                return false;
            }
        }

        public bool TryGetSymbol(IInstrumentName name, ConnectionEnum connection, out IInstrumentInfo info) 
        {
            try
            {
                if (!File.Exists(_instrumentsPath))
                {
                    info = null;
                    return false;
                }

                using var stream = File.OpenRead(_instrumentsPath);
                using var importer = new ExcelImporter(stream);

                var sheet = importer.ReadSheet();
                var infos = sheet.ReadRows<InstrumentInfo>();

                var instrumentInfo = infos.FirstOrDefault(x => x.Name == name.GetFullName() && x.Connection == connection);

                if (instrumentInfo is null) 
                {
                    info = null;
                    return false;
                }

                info = instrumentInfo;
                return true;
            }
            catch (Exception ex)
            {
                info = null;
                return false;
            }
        }
    }
}
