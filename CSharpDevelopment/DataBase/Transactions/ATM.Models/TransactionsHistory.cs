using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class TransactionsHistory
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Ammount { get; set; }
    }
}
