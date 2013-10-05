using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellini.Data.Library
{
    public class ExcelReportGenerator
    {
        public static void GenerateExcelReport(
            string excelFile)
        {
            var reports = GetTotalReportsFromMongoAndLite();
           
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=Excel 12.0 Xml;", 
                excelFile);
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();

                string createTableQuery = "CREATE TABLE Reports(Vendor char(50), Incomes char(20), Expenses char(20), Taxes char(20), Result char(20))";
                OleDbCommand createTableCmd = new OleDbCommand(createTableQuery, con);
                createTableCmd.ExecuteNonQuery();

                foreach (var report in reports)
                {
                    string addReportQuery = "INSERT INTO [Reports$] " +
                                            "VALUES(@vendor, @incomes, @expenses, @taxes, @financialResult)";

                    OleDbCommand addReportCmd = new OleDbCommand(addReportQuery, con);
                    addReportCmd.Parameters.Add(new OleDbParameter("@vendor", report.Vendor.ToString()));
                    addReportCmd.Parameters.Add(new OleDbParameter("@incomes", report.Incomes.ToString()));
                    addReportCmd.Parameters.Add(new OleDbParameter("@expenses", report.Expenses.ToString()));
                    addReportCmd.Parameters.Add(new OleDbParameter("@taxes", report.Taxes.ToString()));
                    addReportCmd.Parameters.Add(new OleDbParameter("@financialResult", report.FinancialResult.ToString()));
                    addReportCmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public static List<ProductTotalReport> GetTotalReportsFromMongoAndLite()
        {
            var productReports = MongoDbProvider.db.LoadData<ProductReport>().ToList();
            WriteProductReportsToSQLite(productReports);
            ProductTaxesDbContext taxesContext = new ProductTaxesDbContext();
            var taxes = taxesContext.ProductTaxes.ToList();
            var vendors = MongoDbProvider.db.LoadData<VendorExpenseMongoDB>().ToList();
            var totalReports = (from p in productReports
                                join t in taxes on p.ProductName equals t.ProductName 
                                join v in vendors on p.VendorName equals v.VendorName
                                select new ProductTotalReport()
                                           {
                                               Expenses = v.Expenses.Sum(e => e.Sum),
                                               Incomes = p.TotalIncome,
                                               Taxes = (double) t.Tax,
                                               Vendor = p.VendorName
                                           }).Distinct(new ReportEqualityComparer()).ToList();
            return totalReports;
        }

        private static void WriteProductReportsToSQLite(List<ProductReport> reports)
        {
            using (ProductTaxesDbContext context = new ProductTaxesDbContext())
            {
                foreach (var report in reports)
                {
                    string query = "INSERT INTO ProductReports VALUES({0}, {1}, {2}, {3}, {4})";
                    context.Database.ExecuteSqlCommand(query,
                                                       report.ProductId, report.ProductName, report.VendorName,
                                                       report.QuantitySold,
                                                       report.TotalIncome);
                }
            }
        }

        private class ReportEqualityComparer : IEqualityComparer<ProductTotalReport>
        {
            public bool Equals(ProductTotalReport x, ProductTotalReport y)
            {
                return x.Vendor == y.Vendor;
            }

            public int GetHashCode(ProductTotalReport obj)
            {
                return obj.Vendor.GetHashCode();
            }
        }
    }
}
