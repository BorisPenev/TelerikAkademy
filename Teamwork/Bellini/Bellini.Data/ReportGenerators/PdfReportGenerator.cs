using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bellini.Data.Library;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Bellini.Data.ReportGenerators
{
    public static class PdfReportGenerator
    {
        public static List<DayReport> GetDayReports()
        {
            var query = SessionState.db.Sales.Include("Supermarket").Include("Products").GroupBy(s => s.Date);
            List<DayReport> reports = new List<DayReport>();
            foreach (var saleDate in query)
            {
                DayReport dayReport = new DayReport();
                dayReport.Sales = new List<PdfSaleReport>();
                dayReport.FormattedDate = saleDate.Key.ToShortDateString();

                double totalSum = 0;
                foreach (var sale in saleDate)
                {
                    PdfSaleReport view = new PdfSaleReport();
                    var product = SessionState.db.Products.Find(sale.ProductId);
                    view.MeasureFormatted = string.Format("{0} {1}",
                                                          sale.Quantity, product.Measure.MeasureName);
                    view.ProductName = product.ProductName;
                    view.Sum = sale.Sum;
                    view.Supermarket = SessionState.db.Supermarkets.Find(sale.SupermarketId).Name; // workaround
                    view.UnitPrice = sale.UnitPrice;
                    totalSum += sale.Sum;
                    dayReport.Sales.Add(view);
                }

                dayReport.TotalSum = totalSum;
                reports.Add(dayReport);
            }

            return reports;
        }

        public static void GeneratePdfReport(string filepath)
        {
            FileStream fileStream = new FileStream(filepath, FileMode.Create);
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, fileStream);
            document.SetPageSize(PageSize.A3);
            document.Open();

            var paragraph = new Paragraph("Aggregated Sales Report",
                FontFactory.GetFont("Arial", 19, Font.BOLD));
            paragraph.SpacingAfter = 20.0f;
            paragraph.Alignment = 1;

            document.Add(paragraph);

            PdfPTable mainTable = new PdfPTable(1);
            var reports = GetDayReports();
            foreach (var dayReport in reports)
            {
                var headerCell = new PdfPCell(new Phrase("Date: " + dayReport.FormattedDate));
                headerCell.BackgroundColor = new BaseColor(175, 166, 166);
                mainTable.AddCell(headerCell);
                var table = GenerateReportTable(dayReport);
                mainTable.AddCell(table);
            }

            document.Add(mainTable);
            document.Close();
        }

        private static PdfPTable GenerateReportTable(DayReport report)
        {
            PdfPTable table = new PdfPTable(5);
            
            string[] headerTitles = {"Product", "Quantity", "Unit Price", "Location", "Sum"};
            foreach (var title in headerTitles)
            {
                Phrase phrase = new Phrase(title);
                phrase.Font = FontFactory.GetFont("Arial", 14, Font.BOLD);
                PdfPCell cell = new PdfPCell(phrase);
                cell.BackgroundColor = new BaseColor(175, 166, 166);
                table.AddCell(cell);
                cell.Padding = 0;
            }

            foreach (var sale in report.Sales)
            {
                table.AddCell(sale.ProductName);
                table.AddCell(sale.MeasureFormatted);
                table.AddCell(sale.UnitPrice.ToString());
                table.AddCell(sale.Supermarket);
                table.AddCell(sale.Sum.ToString());
            }

            PdfPCell footerCell = new PdfPCell(new Phrase("Total sum for " + report.FormattedDate + ": "));
            footerCell.Colspan = 4;
            footerCell.HorizontalAlignment = 2;
            table.AddCell(footerCell);
            table.AddCell(new Phrase(report.TotalSum.ToString()));
            return table;
        }
    }
}
