using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Trading.Exchange.Connections;
using Trading.Exchange.Markets.Core.Instruments;
using Trading.Exchange.Markets.Core.Instruments.Candles;
using Trading.Exchange.Markets.Core.Instruments.Timeframes;
using Trading.Shared.Resolvers;
using System.Diagnostics;
using Trading.Shared.Ranges;
using Trading.Shared.Excel;
using Trading.Exchange.Connections.Storage;

namespace Trading.Api.Services
{
    public class CandleService : ICandleService
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "Services", "Candles");


        private IResolver<ConnectionEnum, IConnection> _connectionResolver;

        public CandleService()
        {
            _connectionResolver = new ConnectionResolver(new BinanceCredentialsProvider());
        }

        public async Task LoadCandlesToFileAsync(CandlesLoadRequest request)
        {
            CreateOrClearDirectory();

            var brokers = request.Brokers.Select(x => _connectionResolver.Resolve(x));
            var tasks = brokers.Select(x => LoadCandlesFromBrokerAsync(x, request.Timeframe, request.Instrument, request.Range));

            await Task.WhenAll(tasks);
        }

        private async Task LoadCandlesFromBrokerAsync(IConnection connection,
            IEnumerable<Timeframes> timeframes,
            IEnumerable<IInstrumentName> instruments,
            IRange<DateTime> range) 
        {
            var instrumentTimeframeZip = instruments.SelectMany(Instrument => timeframes.Select(Timeframe => (Instrument, Timeframe)));

            foreach (var item in instrumentTimeframeZip)
            {
                var c = await connection.GetFuturesCandlesAsync(item.Instrument, item.Timeframe, range);
                await LoadToFile(new CandlesFileName(connection.Type, item.Timeframe, item.Instrument), c);
            }
        }

        private async Task LoadToFile(CandlesFileName name, IEnumerable<ICandle> candles) 
        {
            using (FileStream file = new FileStream(Path.Combine(_path, name.Value()), FileMode.Create, FileAccess.Write))
            {
                var memoryStream = CreateFile(candles);

                byte[] bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                file.Write(bytes, 0, bytes.Length);
                memoryStream.Close();
            }

            await Task.CompletedTask;
        }

        private MemoryStream CreateFile(IEnumerable<ICandle> candles) 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Candles");

            sheet.Cells[1, 1].Value = "Open";
            sheet.Cells[1, 2].Value = "Close";
            sheet.Cells[1, 3].Value = "OpenTime";
            sheet.Cells[1, 4].Value = "CloseTime";
            sheet.Cells[1, 5].Value = "Volume";
            sheet.Cells[1, 6].Value = "High";
            sheet.Cells[1, 7].Value = "Low";

            var positionRow = 2;

            foreach (var candle in candles)
            {
                sheet.Cells[positionRow, 1].Value = candle.Open;
                sheet.Cells[positionRow, 2].Value = candle.Close;
                sheet.Cells[positionRow, 3].Value = candle.OpenTime.ToString();
                sheet.Cells[positionRow, 4].Value = candle.CloseTime.ToString();
                sheet.Cells[positionRow, 5].Value = candle.Volume;
                sheet.Cells[positionRow, 6].Value = candle.High;
                sheet.Cells[positionRow, 7].Value = candle.Low;

                positionRow++;
            }


            sheet.Cells.AutoFitColumns();
            sheet.Protection.IsProtected = true;

            return new MemoryStream(package.GetAsByteArray());
        }

        private void CreateOrClearDirectory() 
        {
            if (Directory.Exists(_path))
            {
                var directoryInfo = new DirectoryInfo(_path);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
            }

            Directory.CreateDirectory(_path);
        }
    }
}
