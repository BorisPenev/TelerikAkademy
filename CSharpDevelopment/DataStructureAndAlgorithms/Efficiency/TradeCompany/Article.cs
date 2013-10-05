using System;

namespace TradeCompany
{
    class Article: IComparable<Article>
    {
        public string Barcode { get; set; }
        public string Vendor { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        
        public Article(string barcode, string vendor, string title, double price)
        {
            this.Barcode = barcode;
            this.Vendor = vendor;
            this.Title = title;
            this.Price = price;
        }

        public int CompareTo(Article other)
        {
            return this.Barcode.CompareTo(other.Barcode);
        }
    }
}
