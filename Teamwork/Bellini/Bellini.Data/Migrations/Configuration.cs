namespace Bellini.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SupermarketSQLDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SupermarketSQLDbContext context)
        {
            using (var dbSuperMarketMysql = new SupermarketModel())
            {
                dbSuperMarketMysql.Vendors.ToList().ForEach(v =>
                {
                    var vendor = new Vendor();
                    vendor.VendorName = v.VendorName;
                    SessionState.db.Vendors.Add(vendor);
                });
                SessionState.db.SaveChanges();
                dbSuperMarketMysql.Measures.ToList().ForEach(m =>
                {
                    Measure measure = new Measure();
                    measure.MeasureName = m.MeasureName;
                    SessionState.db.Measures.Add(measure);
                });
                SessionState.db.SaveChanges();
                dbSuperMarketMysql.Products.ToList().ForEach(p =>
                {
                    string vendorID = p.VendorID.Value.ToString();
                    string measureID = p.MeasureID.Value.ToString();

                    Product product = new Product();
                    product.VendorID = int.Parse(vendorID.Remove(vendorID.Length - 1, 1));
                    product.ProductName = p.ProductName;
                    product.MeasureID = int.Parse(measureID.Remove(measureID.Length - 2, 2));
                    product.BasePrice = p.BasePrice;
                    SessionState.db.Products.Add(product);
                });
                SessionState.db.SaveChanges();
            }
        }
    }
}
