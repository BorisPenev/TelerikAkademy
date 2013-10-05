using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    [XmlRoot("catalog")]
    public class ComplexCatalog
    {
        [XmlElement("book")]
        public List<ComplexBook> Books { get; set; }
    }
}
