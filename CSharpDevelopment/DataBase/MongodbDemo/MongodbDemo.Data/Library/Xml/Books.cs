using System.Collections.Generic;
using System.Xml.Serialization;

namespace MongodbDemo.Data.Library.Xml
{
    [XmlRoot("books")]
    public class Books
    {
        [XmlElement("books")]
        public List<Book> BooklList = new List<Book>();
    }
}
