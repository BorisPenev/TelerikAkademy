using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    [XmlRoot("catalog")]
    public class SampleCatalog
    {
        [XmlElement("book")]
        public List<SampleBook> Books { get; set; }
    }
}
