using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellini.Data.Library
{
    public class Summary
    {
        public DateTime Date { get; set; }
        public float TotalSum { get; set; }

        public Summary()
        {

        }

        public Summary(DateTime date, float totalSum)
        {
            this.Date = date;
            this.TotalSum = totalSum;
        }
    }
}
