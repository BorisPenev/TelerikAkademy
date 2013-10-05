using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellini.Data.Library
{
    public class ProductTotalReport
    {
        public string Vendor { get; set; }
        public double Incomes { get; set; }
        public double Expenses { get; set; }
        public double Taxes { get; set; }
        public double FinancialResult
        {
            get { return this.Incomes - (this.Expenses + this.Taxes); }
        }
    }
}
