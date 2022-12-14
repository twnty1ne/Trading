using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using System.Linq;
using Telegram.Bot.Types.InputFiles;
using Trading.Bot.Sessions;
using Trading.Exchange.Markets.Core.Instruments.Positions;

namespace Trading.Api.Extentions
{
    public static class SessionResultExtentions
    {
        public static InputOnlineFile AsInputOnlineFile(this ISessionBuffer report) 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            var metrics = report.Analytics.GetResults();
            var sheet = package.Workbook.Worksheets.Add("Market Report");

            sheet.Cells[1, 13, 1, 14].Merge = true;
            sheet.Cells[1, 13, 1, 14].Value = "Analytics";
            sheet.Cells[1, 13, 1, 14].Style.Font.Bold = true;


            var metricRow = 2;

            foreach (var metric in metrics)
            {
                sheet.Cells[metricRow, 13].Value = metric.Type;
                sheet.Cells[metricRow, 14].Value = metric.Value;
                metricRow++;
            }

            sheet.Cells[1, 1].Value = "Instrument Name";
            sheet.Cells[1, 2].Value = "Side";
            sheet.Cells[1, 3].Value = "Entry Date";
            sheet.Cells[1, 4].Value = "Entry Price";
            sheet.Cells[1, 5].Value = "Stop Loss";
            sheet.Cells[1, 6].Value = "Take profit";
            sheet.Cells[1, 7].Value = "ROE";
            sheet.Cells[1, 8].Value = "IMR";
            sheet.Cells[1, 9].Value = "Initial Margin";
            sheet.Cells[1, 10].Value = "Result";
            sheet.Cells[1, 11].Value = "Realized PnL";
            sheet.Rows[1].Style.Font.Bold = true;

            var positionRow = 2;
            foreach (var position in report.Positions)
            {
                sheet.Cells[positionRow, 1].Value = position.InstrumentName.GetFullName();
                sheet.Cells[positionRow, 2].Value = position.Side;
                sheet.Cells[positionRow, 3].Value = $"{position.EntryDate.ToShortDateString()} {position.EntryDate.ToShortTimeString()}";
                sheet.Cells[positionRow, 4].Value = position.EntryPrice;
                sheet.Cells[positionRow, 5].Value = position.StopLoss;
                sheet.Cells[positionRow, 6].Value = position.TakeProfit;
                sheet.Cells[positionRow, 7].Value = position.ROE;
                sheet.Cells[positionRow, 8].Value = position.IMR;
                sheet.Cells[positionRow, 9].Value = position.InitialMargin;
                sheet.Cells[positionRow, 10].Value = position.State;
                sheet.Cells[positionRow, 10].Style.Fill.SetBackground(position.State == PositionStates.ClosedByStopLoss ? Color.Red : Color.Green);
                sheet.Cells[positionRow, 11].Value = position.RealizedPnl;
                positionRow++;
            }

            var cellsWithValue = sheet.Cells.Where(x => !string.IsNullOrEmpty(x.Value?.ToString()));

            foreach (var cell in cellsWithValue)
            {

                cell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                cell.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            sheet.Cells.AutoFitColumns();
            sheet.Protection.IsProtected = true;
            return new InputOnlineFile(new MemoryStream(package.GetAsByteArray()), "report.xlsx");
        }
    }
}
