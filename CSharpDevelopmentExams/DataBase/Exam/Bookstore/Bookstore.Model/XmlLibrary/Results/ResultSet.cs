using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bookstore.Model.XmlLibrary.Results
{
    public class ResultSet
    {
        private List<ResultReview> _resultReviews = new List<ResultReview>();

        [XmlElement("review")]
        public List<ResultReview> ResultReviews
        {
            get { return _resultReviews; }
            set { _resultReviews = value; }
        }
    }
}
