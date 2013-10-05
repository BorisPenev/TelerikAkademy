using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    [XmlRoot("query")]
    public class SearchQuery
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("isbn")]
        public long? ISBN { get; set; }
    }
}
