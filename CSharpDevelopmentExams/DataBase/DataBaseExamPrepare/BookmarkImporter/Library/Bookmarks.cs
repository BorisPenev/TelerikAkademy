using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookmarkImporter.Library
{
    [XmlRoot("bookmarks")]
    public class Bookmarks
    {
        [XmlElement("bookmark")]
        public List<Bookmark> bookmarks { get; set; }
    }
}
