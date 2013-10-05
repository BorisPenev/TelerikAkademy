using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    [XmlRoot("query")]
    public class ReviewQuery
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("start-date")]
        public string StartDate { get; set; }

        [XmlElement("end-date")]
        public string EndDate { get; set; }

        [XmlElement("author-name")]
        public string Author { get; set; }
    }
}
