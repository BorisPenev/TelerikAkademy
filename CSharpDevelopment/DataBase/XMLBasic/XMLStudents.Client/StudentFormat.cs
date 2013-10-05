using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace XMLStudents
{
    public class StudentFormat
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("sex")]
        public string Sex { get; set; }
        [XmlElement("birthdate")]
        public DateTime Birthdate { get; set; }
        [XmlElement("phone")]
        public string Phone { get; set; }
        [XmlElement("email")]
        public string eMail { get; set; }
        [XmlElement("course")]
        public int Course { get; set; }
        [XmlElement("specialty")]
        public string Specialty { get; set; }
        [XmlElement("faculty-number")]
        public int FacNumber { get; set; }
        [XmlArray("exams")]
        public List<ExamFormat> Exams { get; set; }
    }
}
