using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Results
{
    [XmlRoot("search-results")]
    public class SearchResult
    {
        private List<ResultSet> _resultSets = new List<ResultSet>();

        [XmlElement("result-set")]
        public List<ResultSet> ResultSets
        {
            get { return _resultSets; }
            set { _resultSets = value; }
        }
    }
}
