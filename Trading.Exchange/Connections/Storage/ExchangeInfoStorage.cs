﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Ganss.Excel;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Ranges;
using Trading.Shared.Ranges.Extensions;

namespace Trading.Exchange.Connections.Storage
{
    internal class ExchangeInfoStorage : IExchangeInfoStorage
    {
        private record PocoCandle 
        {
            public decimal Open { get; set; }
            public decimal Close { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public string OpenTime { get; set; }
            public string CloseTime { get; set; }
            
            public DateTime OpenTimeDate => DateTime.ParseExact(OpenTime, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture); 
            public DateTime CloseTimeDate => DateTime.ParseExact(CloseTime, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture); 
            public decimal Volume { get; set; }
        }

        private readonly string _candlesPath = 
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Connections", "Storage", "Candles");

        private readonly string _instrumentsPath = 
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Connections", "Storage", "Instruments", 
                "instruments_info.xlsx");

        public ExchangeInfoStorage()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public bool TryGetCandles(IInstrumentName name, ConnectionEnum connection, Timeframes timeframe, 
            out IEnumerable<Candle> candles, IRange<DateTime> range)
        {
            try
            {
                var monthsBetween = range.MonthsBetween();
                
                var fileNames = monthsBetween
                    .Select(x => new CandlesFileName(connection, timeframe, name, x.Year, x.Month));

                 candles = fileNames.SelectMany(ReadFile).ToList();
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
                
                var mapper = new ExcelMapper(_instrumentsPath);
                
                var infos = mapper.Fetch<InstrumentInfo>();

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

        private IEnumerable<Candle> ReadFile(CandlesFileName name)
        {
            var filePath = Path.Combine(_candlesPath, name.Value());
            var excelMapper = new ExcelMapper(filePath);
            
            excelMapper.Ignore<PocoCandle>(x => x.OpenTimeDate);
            excelMapper.Ignore<PocoCandle>(x => x.CloseTimeDate);
            
            var pocoCandles = excelMapper.Fetch<PocoCandle>();

            return pocoCandles.Select(x => new Candle(x.Open, x.Close, x.High, x.Low, x.Volume, x.OpenTimeDate, x.CloseTimeDate))
                .OrderBy(x => x.OpenTime)
                .ToList();
        }
    }
}
