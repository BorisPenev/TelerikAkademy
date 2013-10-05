using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary
{
    public class Review
    {
        [XmlAttribute("author")]
        public string Author { get; set; }

        [XmlAttribute("date")]
        public string CreatDate { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
