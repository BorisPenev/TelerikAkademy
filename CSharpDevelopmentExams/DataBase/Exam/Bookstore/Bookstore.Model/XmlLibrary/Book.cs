using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary
{
    public class Book
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("isbn")]
        public long? ISBN { get; set; }

        [XmlElement("price")]
        public double? Price { get; set; }

        [XmlElement("web-site")]
        public string Url { get; set; }
    }
}
