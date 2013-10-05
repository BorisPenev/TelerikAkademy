using System.Collections.Generic;

namespace Bellini.Data.Library
{
    public class DayReport
    {
        public string FormattedDate { get; set; }
        public List<PdfSaleReport> Sales { get; set; }
        public double TotalSum { get; set; }
    }
}
