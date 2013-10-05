using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary
{
    public class Reviews
    {
        [XmlElement("review")]
        public List<Review> reviews { get; set; }
    }
}
