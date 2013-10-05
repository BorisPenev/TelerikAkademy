using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMLStudents.Client;
using XMLStudents.Data;

namespace XMLStudents
{
    class Program
    {
        static void Main()
        {
            StudentsToXML stud = new StudentsToXML();
            stud.StudentsList = new List<StudentFormat>();

            XMLStudentsEntities context = new XMLStudentsEntities();

            context.Students.ToList().ForEach(s =>
            {
                var student = new StudentFormat();
                student.eMail = s.eMail;
                student.Birthdate = s.Birthday;
                student.Course = s.Course;
                student.FacNumber = s.FacultyNumber;
                student.Name = s.Name;
                student.Phone = s.Phone;
                student.Sex = s.Sex == "F" ? "Female" : "Male";
                student.Specialty = s.Specialty;
                List<ExamFormat> examList = new List<ExamFormat>();
                s.Exams.ToList().ForEach(e =>
                    {
                        var examFormat = new ExamFormat();
                        examFormat.Name = e.Name;
                        examFormat.Tutor = e.Tutor;
                        examFormat.Score = e.Score;

                        examList.Add(examFormat);
                    });
                student.Exams = new List<ExamFormat>();
                student.Exams = examList;

                stud.StudentsList.Add(student);
            });
            SerializeToXml(stud, @"..\..\..\XML_export\test.xml");
        }

        public static void SerializeToXml<T>(T obj, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var ser = new XmlSerializer(typeof(T));
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                ser.Serialize(fileStream, obj, xns);
            }
        }

        public static T DeserializeFromXml<T>(string filePath)
        {
            T result;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var ser = new XmlSerializer(typeof(T));
                result = (T)ser.Deserialize(fs);
            }
            return result;
        }
    }
}
