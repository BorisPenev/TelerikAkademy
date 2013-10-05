using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Results
{
    public class ResultBook
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("authors")]
        public string Authors { get; set; }
        public bool ShouldSerializeAuthors()
        {
            return !string.IsNullOrEmpty(Authors);
        }

        [XmlElement("isbn")]
        public long? ISBN { get; set; }
        public bool ShouldSerializeISBN()
        {
            return ISBN.HasValue;
        }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}
