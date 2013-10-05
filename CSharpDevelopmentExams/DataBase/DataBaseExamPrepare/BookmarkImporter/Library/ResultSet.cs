using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookmarkImporter.Library
{
    public class ResultSet
    {
        private List<Bookmark> _bookmarks = new List<Bookmark>();

        [XmlElement("bookmark")]
        public List<Bookmark> Bookmarks
        {
            get { return _bookmarks; }
            set { _bookmarks = value; }
        }
    }
}
