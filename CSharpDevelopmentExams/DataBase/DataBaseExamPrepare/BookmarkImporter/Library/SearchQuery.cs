using System.Xml.Serialization;

namespace BookmarkImporter.Library
{
    [XmlRoot("query")]
    public class SearchQuery
    {
        [XmlElement("username")]
        public string Username { get; set; }
        [XmlElement("tag")]
        public string Tag { get; set; }
    }
}
