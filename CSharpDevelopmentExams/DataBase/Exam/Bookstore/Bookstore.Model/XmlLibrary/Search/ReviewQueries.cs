using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Search
{
    [XmlRoot("review-queries")]
    public class ReviewQueries
    {
        [XmlElement("query")]
        public List<ReviewQuery> ReviewQueryList { get; set; }

        
    }
}
