using System.Xml.Serialization;

namespace MongodbDemo.Data.Library
{
    //[XmlRoot("Bookmark")] 
    public class Bookmark
    {
        [XmlElement("username")]
        public string Username { get; set; }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("url")]
        public string Url { get; set; }
        [XmlElement("tag")]
        public string Tag { get; set; }
        [XmlElement("notes")]
        public string Notes { get; set; }
    }
}
