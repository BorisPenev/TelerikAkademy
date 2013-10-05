using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellini.Data.Library
{
    public class SummaryReport
    {
        public string VendorName { get; set; }
        public List<Summary> Summaries { get; set; }

        public SummaryReport()
        {

        }

        public SummaryReport(string vendorName, List<Summary> summaries)
        {
            this.VendorName = vendorName;
            this.Summaries = summaries;
        }
    }
}
