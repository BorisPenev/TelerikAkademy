using System;

namespace Bookstore.Model
{
    public class SearchLogs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string QueryXml { get; set; }
    }
}
