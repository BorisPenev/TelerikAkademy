using System;
using System.Xml.Serialization;

namespace MongodbDemo.Data.Library.Xml
{
    public class Book
    {
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlElement("author")]
        public string Author { get; set; }
        [XmlElement("publish-date")]
        public string PublishDate { get; set; }
    }
}
