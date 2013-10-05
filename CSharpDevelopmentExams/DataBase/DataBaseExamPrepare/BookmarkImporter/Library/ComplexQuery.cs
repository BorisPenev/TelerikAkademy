using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookmarkImporter.Library
{
    [XmlRoot("queries")]
    public class ComplexQuery
    {
        [XmlElement("query")]
        public List<Query> Query { get; set; }
    }
}
