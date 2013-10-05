using System.Xml.Serialization;

namespace BookmarkImporter.Library
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
        [XmlElement("tags")]
        public string Tags { get; set; }
        [XmlElement("notes")]
        public string Notes { get; set; }
    }
}
