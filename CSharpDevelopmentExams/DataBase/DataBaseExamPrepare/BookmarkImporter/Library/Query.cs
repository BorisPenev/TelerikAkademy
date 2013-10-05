using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookmarkImporter.Library
{
    [XmlRoot("query")]
    public class Query
    {
        [XmlElement("username")]
        public string Username { get; set; }
        [XmlElement("tag")]
        public List<string> Tag { get; set; }

        [XmlAttribute("max-results")]
        public int MaxResults { get; set; }
    }
}
