using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookmarkImporter.Library
{
    [XmlRoot("search-results")]
    public class SearchResults
    {
        private List<ResultSet> _resultSet = new List<ResultSet>();

        [XmlElement("result-set")]
        public List<ResultSet> ResultSet
        {
            get { return _resultSet; }
            set { _resultSet = value; }
        }
    }
}
