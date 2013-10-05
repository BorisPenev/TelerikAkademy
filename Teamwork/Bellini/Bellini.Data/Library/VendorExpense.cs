using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bellini.Library;

namespace Bellini.Data.Library
{
    public partial class VendorExpense
    {
        
        public string VendorName { get; set; }

        private ICollection<Expense> _expenses = new Collection<Expense>();
        public virtual ICollection<Expense> Expenses
        {
            get { return _expenses; }
            set { _expenses = value; }
        }
    }
}
