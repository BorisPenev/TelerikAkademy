using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary
{
    public class Author
    {
        [XmlText]
        public string Name { get; set; }
    }
}
