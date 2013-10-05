using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bellini.Data
{
    public class Sale
    {
        public int ID { get; set; }

        public int ProductId { get; private set; }
        public Product Product { get; set; }

        public int Quantity { get; private set; }
        public double UnitPrice { get; private set; }

        public int SupermarketId { get; set; }
        public Supermarket Supermarket { get; set; }

        public DateTime Date { get; set; }

        public double Sum 
        { 
            get { return this.Quantity*this.UnitPrice; } 
        }

        public Sale()
        {
            
        }

        public Sale(int productId, int quantity, double unitPrice)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
    }
}
