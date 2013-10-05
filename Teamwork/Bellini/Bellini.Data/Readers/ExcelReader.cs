using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace Bellini.Data.Readers
{
    public static class ExcelReader
    {

        //public static void UnzipExcelFiles(string outputPath)
        //{
        //    string zipPath = "..\\..\\Sample-Sales-Reports.zip";
        //    string outPath = "outputPath";
        //    //ZipUtil.UnZipFiles(zipPath, outPath, string.Empty, false);
        //}

        public static Dictionary<DateTime, List<Supermarket>> GetSupermarketReportsByDate(string reportsPath)
        {
            DirectoryInfo mainDir = new DirectoryInfo(reportsPath);
            var subDirectories = mainDir.GetDirectories();
            Dictionary<DateTime, List<Supermarket>> output = new Dictionary<DateTime, List<Supermarket>>();

            foreach (var subDir in subDirectories)
            {
                var supermarkets = Read(subDir.FullName);
                output.Add(DateTime.Parse(subDir.Name), supermarkets);
            }

            return output;
        }

        public static List<Supermarket> Read(string path)
        {
            List<Supermarket> supermarkets = new List<Supermarket>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            foreach (var file in dirInfo.EnumerateFiles("*.xls"))
            {
                string connectionstring = string.Format(
                    "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"",
                    file.FullName);
                OleDbConnection oleDbConnection = new OleDbConnection(connectionstring);
                oleDbConnection.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sales$]", oleDbConnection);
                var reader = cmd.ExecuteReader();

                // skip the empty rows
                reader.Read();
                reader.Read();
                string location = reader[0].ToString();
                var supermarket = SessionState.db.Supermarkets.FirstOrDefault(s => s.Name == location);
                if (supermarket == null)
                {
                    supermarket = new Supermarket();
                    supermarket.Name = location;
                    supermarket.Sales = new Collection<Sale>();
                    SessionState.db.Supermarkets.Add(supermarket);
                }

                while (reader.Read())
                {
                    if (!reader[0].ToString().Contains("Total"))
                    {
                        int productId;
                        int quantity;
                        double unitPrice;

                        bool productIdSuccess = int.TryParse(reader[0].ToString(), out productId);
                        bool quantitySuccess = int.TryParse(reader[1].ToString(), out quantity);
                        bool unitPriceSuccess = double.TryParse(reader[2].ToString(), out unitPrice);

                        if (productIdSuccess && quantitySuccess && unitPriceSuccess)
                        {
                            //sales.Add(new Sale(productId, quantity, unitPrice, location));
                            Sale sale = new Sale(productId, quantity, unitPrice);
                            sale.Date = DateTime.Parse(dirInfo.Name);
                            supermarket.Sales.Add(sale);
                        }
                    }
                }

                supermarkets.Add(supermarket);
                reader.Close();
                reader.Dispose();

                oleDbConnection.Close();
                oleDbConnection.Dispose();
            }

            SessionState.db.SaveChanges();
            return supermarkets;
        }
    }
}
