using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    public class ComplexBook: XmlLibrary.Book
    {
        [XmlElement("authors")]
        public Authors Authors { get; set; }

        [XmlElement("reviews")]
        public Reviews Reviews { get; set; }
    }
}
