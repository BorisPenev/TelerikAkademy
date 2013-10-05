using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Bellini.Data.Library
{
    public class ProductReport
    {
        public ObjectId Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public int QuantitySold { get; set; }
        public double TotalIncome { get; set; }
        public ProductReport()
        {

        }
        public ProductReport(int productId, string productName, string vendorName, int quantitySold, double totalIncome)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.VendorName = vendorName;
            this.QuantitySold = quantitySold;
            this.TotalIncome = totalIncome;
        }
    }
}
