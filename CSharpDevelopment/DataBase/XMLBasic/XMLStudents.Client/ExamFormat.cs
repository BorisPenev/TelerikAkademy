using System;
using System.Linq;
using System.Xml.Serialization;

namespace XMLStudents
{
    [XmlRoot("exam")]
    public class ExamFormat
    {
        public string Name { get; set; }

        public string Tutor { get; set; }

        public double? Score { get; set; }
        public bool ShouldSerializeScore()
        {
            return Score.HasValue;
        }
    }
}
