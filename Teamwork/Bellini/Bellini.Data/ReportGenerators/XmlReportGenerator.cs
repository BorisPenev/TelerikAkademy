using System;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bellini.Data.ReportGenerators
{
    public static class XmlReportGenerator
    {
        public static void GenerateXmlReport(string filePath)
        {
            var dbResult = (from sales in SessionState.db.Sales
                            select new
                            {
                                vendorName = sales.Product.Vendor.VendorName,
                                date = sales.Date,
                                sum = sales.Quantity * sales.Product.BasePrice

                            }).GroupBy(g => g.vendorName).ToDictionary(k => k.Key, k => k.GroupBy(g => g.date));

            Encoding encoding = Encoding.GetEncoding("windows-1251");
            using (XmlTextWriter writer = new XmlTextWriter(filePath, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("sales");
                foreach (var vendor in dbResult)
                {
                    writer.WriteStartElement("sale");
                    writer.WriteAttributeString("vendor", vendor.Key);
                    foreach (var date in vendor.Value)
                    {
                        var sum = date.ToList().Select(s => s.sum).Sum();

                        writer.WriteStartElement("summary");
                        writer.WriteAttributeString("date", date.Key.ToString());
                        writer.WriteAttributeString("total-sum", sum.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                Console.WriteLine("Document {0} created.", filePath);
            }

        }
    }
}
