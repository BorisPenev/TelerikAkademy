using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class CardAccount
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public string CardPin { get; set; }

        public decimal CardCash { get; set; }
    }
}
