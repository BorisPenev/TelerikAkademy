using System.Collections.Generic;
using System.Xml.Serialization;

namespace MongodbDemo.Data.Library
{
    [XmlRoot("bookmarks")]
    public class Bookmarks
    {
        [XmlElement("bookmark")]
        public List<Bookmark> bookmarks { get; set; }
    }
}
