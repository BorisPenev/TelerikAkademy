using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    public class SampleBook: XmlLibrary.Book
    {
        [XmlElement("author")]
        public string Author { get; set; }
    }
}
