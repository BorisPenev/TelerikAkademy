using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary
{
    public class Authors
    {
        [XmlElement("author")]
        public List<Author> Author { get; set; }
    }
}
