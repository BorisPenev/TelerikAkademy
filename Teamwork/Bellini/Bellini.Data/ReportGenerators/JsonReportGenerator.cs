using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Bellini.Data.Library;
using Bellini.Library;

namespace Bellini.Data.ReportGenerators
{
    public static class JsonReportGenerator
    {
        public static List<ProductReport> GenerateProductReport()
        {
            string query = @"Select s.ProductId,p.ProductName,v.VendorName,SUM(s.Quantity) as QuantitySold,SUM(s.Quantity*s.UnitPrice) as TotalIncome
                            From Sales s
                            INNER JOIN Products p ON s.ProductId=p.ID
                            INNER JOIN Vendors v ON v.ID=p.VendorID
                            Group By s.ProductId,p.ProductName,v.VendorName";
            var queryResult = SessionState.db.Database.SqlQuery<ProductReport>(query).ToList();

            return queryResult;
        }

        public static void GenerateJsonReports(string reportDir)
        {
            if (Helpers.DirectoryExist(reportDir))
            {
                List<ProductReport> productReport = GenerateProductReport();
                MongoDbProvider.db.BatchInsert(productReport);

                foreach (var report in productReport)
                {
                    var json = new JavaScriptSerializer().Serialize(report);
                    var jsonPath = reportDir + report.ProductId + ".json";
                    File.WriteAllText(jsonPath, json);
                }
            }
        }
    }
}
