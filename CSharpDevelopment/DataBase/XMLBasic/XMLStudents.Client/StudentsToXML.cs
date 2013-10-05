using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace XMLStudents.Client
{
    [XmlRoot("students")]
    public class StudentsToXML
    {
        [XmlElement("student")]
        public List<StudentFormat> StudentsList { get; set; }

        //[XmlElement("exams")]
        //public List<ExamFormat> ExamsList { get; set; }
    }
}
