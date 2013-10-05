using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Results
{
    public class ResultReview
    {
        [XmlElement("date")]
        public string CreatDate { get; set; }

        [XmlElement("content")]
        public string Text { get; set; }

        [XmlElement("book")]
        public ResultBook ResultBook { get; set; }
    }
}
